using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] ParticleSystem _weaponFlash;
    [SerializeField] AudioClip _weaponFire;
    [SerializeField] Camera _mainCamera;
    [SerializeField] Transform _rayOrigin;

    RaycastHit rayHit;

    public float shootDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _weaponFlash.Play();
            AudioSource.PlayClipAtPoint(_weaponFire, transform.position);
            Shoot();
        }
    }

    void Shoot()
    {
        //calculate direction for ray
        Vector3 rayDirection = _mainCamera.transform.forward;
        //cast a debug ray
        Debug.DrawRay(_rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);
        //do ray
        if (Physics.Raycast(_rayOrigin.position, rayDirection, out rayHit, shootDistance))
            Debug.Log("Ray hit a " + rayHit.transform.gameObject);
        else
            Debug.Log("Miss");

    }
}
