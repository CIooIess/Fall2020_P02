using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{   
    [SerializeField] GameObject _enemyBullet;
    [SerializeField] AudioClip _enemyHurtSFX;
    Transform _playerTarget;
    [SerializeField] Image _enemyHealthViewImage;
    [SerializeField] Text _enemyDamageNumber;

    public float enemyHealth = 100f;
    float healthMax;
    public float engageDistance = 2f;
    public float fireDelay = 1f;

    bool isFiring = false;

    void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        healthMax = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_playerTarget.position, transform.position) < engageDistance)
        {
            transform.LookAt(_playerTarget);
            if (isFiring == false)
            {
                isFiring = true;
                StartCoroutine("BulletFire");
            }
        } else
        {
            isFiring = false;
            StopCoroutine("BulletFire");
        }
    }

    public void HealthChange(float changeAmount)
    {
        StartCoroutine(DamageNumber(changeAmount));

        if (enemyHealth > 0)
        {
            enemyHealth += changeAmount;
        }
        if (enemyHealth > healthMax)
        {
            enemyHealth = healthMax;
        }
        if (enemyHealth < 0)
        {
            enemyHealth = 0;
        }

        AudioSource.PlayClipAtPoint(_enemyHurtSFX, transform.position);

        _enemyHealthViewImage.rectTransform.localScale = new Vector3(enemyHealth / healthMax, 1f, 1f);

        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageNumber(float changeAmount)
    {
        _enemyDamageNumber.gameObject.SetActive(true);
        _enemyDamageNumber.text =
            changeAmount.ToString();
        yield return new WaitForSeconds(0.1f);
        _enemyDamageNumber.gameObject.SetActive(false);
    }

    IEnumerator BulletFire()
    {
        while (Vector3.Distance(_playerTarget.position, transform.position) < engageDistance)
        {
            yield return new WaitForSeconds(fireDelay);
            Instantiate(_enemyBullet, gameObject.transform);
        }
        yield return null;
    }
}
