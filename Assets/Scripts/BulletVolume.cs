using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVolume : MonoBehaviour
{
    Rigidbody rb;

    public int damage;
    public float moveSpeed = 100f;
    public float lifeTime = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * moveSpeed);
        gameObject.transform.parent = null;
        StartCoroutine("Life");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.Player.HealthChange(-Mathf.Abs(damage));
        }
        if (!other.CompareTag("Enemy") && !other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
