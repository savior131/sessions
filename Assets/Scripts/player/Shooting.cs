using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Shooting : MonoBehaviour
{
    Camera cam;
    Vector2 mousePos;
    [SerializeField] Transform shoulder;
    [SerializeField] float shootDelay;
    [SerializeField] Transform gunBarrel;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    Vector2 dir;
    Rigidbody2D rb;
    Animator anim;
    bool canShoot = true;
    void Start()
    {
        cam=Camera.main;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AimHandler();
        AnimationHandler();
        HandelShooting();
    }
    private void AimHandler()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        dir = mousePos-(Vector2)shoulder.position;
        float rotation=Mathf.Atan2 (dir.y, dir.x)*Mathf.Rad2Deg;
        shoulder.transform.rotation = Quaternion.Euler(0,0, rotation);
    }
    private void AnimationHandler()
    {
        if (transform.position.x <= mousePos.x)
        {
            Flip(false);
            if (rb.velocity.x >= 0)
            {
                anim.SetBool("reverse", false);
            }
            else
            {
                anim.SetBool("reverse", true);
            }
        }
        else
        {
            Flip(true);
            if (rb.velocity.x >= 0)
            {
                anim.SetBool("reverse", true);
            }
            else
            {
                anim.SetBool("reverse", false);
            }
        }
    }
    private void Flip(bool flip)
    {
        if (flip) {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void HandelShooting()
    {
        if (!canShoot) return;
        if(Input.GetMouseButton(0))
        {
            instantiateBullet();
            StartCoroutine("delayShooting");
        }
    }
    private void instantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.transform.position, shoulder.transform.rotation);
        dir.Normalize();
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }

    IEnumerator delayShooting()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
