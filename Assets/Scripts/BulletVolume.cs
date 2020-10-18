using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVolume : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;

    public int damage;
    public float moveSpeed = 100f;
    public float lifeTime = 10f;

    private void Start()
    {
        gameObject.transform.parent = null;
        _rb.AddForce(transform.forward * moveSpeed);
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
