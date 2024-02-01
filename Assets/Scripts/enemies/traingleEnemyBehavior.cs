using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traingleEnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    Transform player;
    [SerializeField] float dis;
    Vector2 dir;
    float rot;
    [SerializeField] bool canshoot = true;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shootDelay;
    [SerializeField] Transform shootPos;
    Vector2 offset;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector2(Random.Range(-3, 3), Random.Range(5, 10));
    }

    private void Update()
    {
        moveToPlayer();
        handleAming();
        handleShooting();
    }
    void handleAming()
    {
        dir = player.transform.position - transform.position;
        rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    void handleShooting()
    {
        if (!canshoot) return;
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPos.transform.position, transform.rotation);
            dir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
            StartCoroutine("dlayshooting");
        }
    }

    void moveToPlayer()
    {

        float dis = Vector3.Distance(transform.position, player.position);
        if (dis > this.dis)
        {
            Vector2 playerPos = (Vector2)player.position + offset;
            transform.position = Vector2.MoveTowards(transform.position,playerPos, speed * Time.deltaTime);
        }
    }

    IEnumerator dlayshooting()
    {
        canshoot = false;
        yield return new WaitForSeconds(shootDelay);
        canshoot = true;
    }
}


