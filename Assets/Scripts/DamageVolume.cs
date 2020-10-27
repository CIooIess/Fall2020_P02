using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    Collider player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            PlayerHealth.Player.HealthChange(-Mathf.Abs(damage));
        }
    }
}
