using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDeleter : MonoBehaviour
{
    Transform player;
    public float renderDistance = 120f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 1)
        {
            if (Vector3.Distance(transform.position, player.position) > renderDistance)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
            }
        }
    }
}
