using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTrailBurst : MonoBehaviour
{
    public float life = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("LifeTime");
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
