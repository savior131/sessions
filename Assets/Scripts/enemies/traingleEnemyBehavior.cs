using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traingleEnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    Transform player;
    [SerializeField] float disY;
    [SerializeField] float distance;
    Vector2 dis;
    float rot;
    [SerializeField] bool canshoot = true;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shootDelay;
    [SerializeField] Transform shootPos;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        moveToPlayer();
        handleAming();
        handleShooting();
    }
    void handleAming()
    {
        dis = player.transform.position - transform.position;
        rot = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    void handleShooting()
    {
        if (!canshoot) return;
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPos.transform.position, transform.rotation);
            dis.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = dis * bulletSpeed;
            StartCoroutine("dlayshooting");
        }
    }

    void moveToPlayer()
    {

        float dis = Vector3.Distance(transform.position, player.position);
        if (dis > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        if (transform.position.y < disY)
        {
            transform.position = new Vector2(transform.position.x, disY);
        }
    }

    IEnumerator dlayshooting()
    {
        canshoot = false;
        yield return new WaitForSeconds(shootDelay);
        canshoot = true;
    }
}


