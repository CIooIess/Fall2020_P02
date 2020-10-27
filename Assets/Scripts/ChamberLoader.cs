using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberLoader : MonoBehaviour
{
    [SerializeField] GameObject[] _chambers;

    public float renderRadius = 40;
    Collider[] nodes;

    void Update()
    {
        nodes = Physics.OverlapSphere(transform.position, renderRadius);

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].CompareTag("Node") && nodes[i].transform.childCount == 0)
            {
                Debug.Log(nodes + "detected");
                int r = Mathf.FloorToInt(Random.Range(0, _chambers.Length));
                Debug.Log(r);
                int rot = Mathf.FloorToInt(Random.Range(0, 4));
                Instantiate(_chambers[r], nodes[i].transform.position, Quaternion.Euler(0,rot*90,0), nodes[i].transform);
            }
        }
    }
}
