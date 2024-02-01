using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class OctalBehavior : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] shootPoints;
    [SerializeField] Transform octal;
    bool canShoot = true;
    [SerializeField] float timeDelay;
    [SerializeField] float speed;

    private void Update()
    {
        handelShooting();
    }

    void handelShooting()
    {
        if(!canShoot) { return; }
        else
        {
            foreach(Transform points in shootPoints)
            {
                Vector2 dir = points.position - transform.position;
                dir.Normalize();
                GameObject bullet = Instantiate(bulletPrefab , points.position ,octal.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * speed;
                StartCoroutine(shootDelay());
            }
        }
    }

    IEnumerator shootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeDelay);
        canShoot = true;
    }
}
