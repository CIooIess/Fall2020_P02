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
                int r = Random.Range(0, _chambers.Length - 1);
                Debug.Log(r);
                Instantiate(_chambers[r], nodes[i].transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("deleted" + other);
        Destroy(other.GetComponentInChildren<Transform>().gameObject);
    }
}
