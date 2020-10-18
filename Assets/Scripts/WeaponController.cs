using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField] ParticleSystem _weaponFlash;
    [SerializeField] ParticleSystem _weaponCharge;
    [SerializeField] Object _weaponTrail;
    [Space(10)]
    [SerializeField] AudioClip _weaponFireSFX;
    [SerializeField] AudioClip _weaponBigFireSFX;
    [SerializeField] AudioClip _weaponChargeSFX;
    [SerializeField] AudioClip _weaponHitSFX;
    [Space(10)]
    [SerializeField] Image _chargeBarViewImage;
    [SerializeField] Text _chargeBarViewText;
    [Space(10)]
    [SerializeField] Camera _mainCamera;
    [SerializeField] Transform _rayOrigin;
    [Space(10)]
    
    RaycastHit rayHit;

    public float shootDistance = 10f;
    public float weaponDamage = 10;

    bool cooling;
    public float cooldown = 1f;
    float charge = 0;
    public float chargeMax = 20f;
    public float chargeIncrease = 1;
    public float chargeRate = 0.5f;

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
                    charge += chargeIncrease;
                if (charge > chargeMax)
                    charge = chargeMax;

                UIUpdate();

                yield return new WaitForSeconds(chargeRate);
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
        if (charge < chargeMax / 1.2f)
            AudioManager.Instance.PlaySFX(_weaponFireSFX, 1);
        else
            AudioManager.Instance.PlaySFX(_weaponBigFireSFX, 2);

        //calculate direction for ray
        Vector3 rayDirection = _mainCamera.transform.forward;
        //cast a debug ray
        Debug.DrawRay(_rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);
        //do ray
        if (Physics.Raycast(_mainCamera.transform.position, rayDirection, out rayHit, shootDistance))
        {
            Debug.Log("Ray hit a " + rayHit.transform.name);

            float step = 1f / 30f;
            float currentStep = 0;
            for (int i = 0; i < 30f; i++)
            {
                currentStep += step;
                Vector3 position = Vector3.Lerp(_rayOrigin.position, rayHit.point, currentStep);
                Instantiate(_weaponTrail, position, Quaternion.Euler(0, 0, 0));
            }

            AudioSource.PlayClipAtPoint(_weaponHitSFX, rayHit.point);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                EnemyController hitEnemy = rayHit.collider.gameObject.GetComponent<EnemyController>();
                hitEnemy.HealthChange(-Mathf.Abs(weaponDamage + charge));
            }
        }
        else
        {
            float step = 1f / 30f;
            float currentStep = 0;
            for (int i = 0; i < 30f; i++)
            {
                currentStep += step;
                Vector3 position = Vector3.Lerp(_rayOrigin.position, _mainCamera.transform.position + rayDirection * shootDistance, currentStep);
                Instantiate(_weaponTrail, position, Quaternion.Euler(0, 0, 0));
            }
            Debug.Log("Miss");
        }

        charge = 0;
        UIUpdate();
        cooling = true;
        StartCoroutine("Cooldown");
    }
}
