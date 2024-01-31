using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float timeToLive;
    private void Start()
    {
        StartCoroutine(destory());
    }

    IEnumerator destory()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
