using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinesColorChange : MonoBehaviour
{
    [SerializeField] bool useCoroutins;
    bool wait;
    [SerializeField] float delay;
    SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(useCoroutins)
        {
            if (!wait)
            {
                StartCoroutine(colorChangeDelay());
            }
        }
        else
        {
            StopCoroutine("colorChangeDelay");
            sp.color = Random.ColorHSV();
        }
        
    }
    IEnumerator colorChangeDelay()
    {
        wait = true;
        yield return new WaitForSeconds(delay);
        sp.color = Random.ColorHSV();
        wait = false;
    }
}
