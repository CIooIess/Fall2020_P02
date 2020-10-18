using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField] ParticleSystem _weaponFlash;
    [SerializeField] ParticleSystem _weaponCharge;
    [Space(10)]
    [SerializeField] AudioClip _weaponFire;
    [SerializeField] AudioClip _weaponChargeSFX;
    [Space(10)]
    [SerializeField] Image _chargeBarViewImage;
    [SerializeField] Text _chargeBarViewText;
    [Space(10)]
    [SerializeField] Camera _mainCamera;
    [SerializeField] Transform _rayOrigin;
    [SerializeField] Light _pointLight;
    [Space(10)]
    
    RaycastHit rayHit;

    public float shootDistance = 10f;

    bool cooling;
    public float cooldown = 1f;
    float charge = 0f;
    public float chargeMax = 20f;
    public float chargeRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        UIUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1") && !cooling)
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("Charge");
        }
    }

    IEnumerator Charge()
    {
        yield return new WaitForSeconds(0.2f);
        if (Input.GetButton("Fire1"))
        {
            _weaponCharge.Play();
            while (Input.GetButton("Fire1"))
            {
                AudioManager.Instance.PlaySFX(_weaponChargeSFX, 1);

                if (charge < chargeMax)
                    charge += chargeRate;
                if (charge > chargeMax)
                    charge = chargeMax;

                UIUpdate();

                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return null;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        cooling = false;
    }

    void UIUpdate()
    {
        _chargeBarViewText.text =
            "WEAPON_CHARGE = " + charge.ToString() + "/" + chargeMax.ToString() + " <";
        _chargeBarViewImage.rectTransform.localScale = new Vector3(charge / chargeMax, 1f, 1f);
    }

    void Shoot()
    {
        StopAllCoroutines();
        _weaponCharge.Clear();
        _weaponCharge.Stop();
        _weaponFlash.Play();
        AudioManager.Instance.PlaySFX(_weaponFire, 1);

        //calculate direction for ray
        Vector3 rayDirection = _mainCamera.transform.forward;
        //cast a debug ray
        Debug.DrawRay(_rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);
        //do ray
        if (Physics.Raycast(_rayOrigin.position, rayDirection, out rayHit, shootDistance))
        {
            Debug.Log("Ray hit a " + rayHit.transform.name);
            _pointLight.transform.position = rayHit.point;
        }
        else
            Debug.Log("Miss");

        charge = 0;
        UIUpdate();
        cooling = true;
        StartCoroutine("Cooldown");
    }
}
