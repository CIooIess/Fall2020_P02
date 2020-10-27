using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickup : MonoBehaviour
{
    [SerializeField] AudioClip _pointSFX;

    public int pointAmount = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Level01Controller.Level01.IncreaseScore(Mathf.Abs(pointAmount));
            AudioManager.Instance.PlaySFX(_pointSFX, 1);
            Destroy(gameObject);
        }
    }
}
