using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemeyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 dir;
    Transform player;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        findDirctionPlayer();
        setSpeed();
    }

    void findDirctionPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dir = player.transform.position - transform.position;
    }
    void setSpeed()
    {
        rb.velocity = dir.normalized * speed;
    }
}
