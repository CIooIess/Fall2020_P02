using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    [SerializeField] CharacterController _player;

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other == _player)
        {
            PlayerHealth.Player.HealthChange(-Mathf.Abs(damage));
        }
    }
}
