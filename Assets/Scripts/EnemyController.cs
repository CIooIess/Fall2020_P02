using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{   
    [SerializeField] GameObject _enemyBullet;
    [SerializeField] Object _deathParticles;
    [Space(10)]
    [SerializeField] AudioClip _enemyFireSFX;
    [SerializeField] AudioClip _enemyHurtSFX;
    [SerializeField] AudioClip _enemyDieSFX;
    [Space(10)]
    [SerializeField] Image _enemyHealthViewImage;
    [SerializeField] Text _enemyDamageNumber;

    Transform _playerTarget;

    public float moveSpeed = 1f;
    public float enemyHealth = 100f;
    public int score = 10;
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
            transform.position = Vector3.MoveTowards(transform.position, _playerTarget.position, moveSpeed * Time.deltaTime);
            if (isFiring == false)
            {
                isFiring = true;
                StartCoroutine("BulletFire");
            }
        } else
        {
            isFiring = false;
            StopCoroutine("BulletFire");

            /*
            if (Random.Range(0, 1000) == 500)
            {
                transform.Rotate(Random.Range(-90,90), Random.Range(0, 360), transform.rotation.eulerAngles.z);
            }
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 1, moveSpeed * Time.deltaTime);
            */
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
            Level01Controller.Level01.IncreaseScore(score);
            AudioSource.PlayClipAtPoint(_enemyDieSFX, transform.position);
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
            Instantiate(_enemyBullet, transform);
            AudioSource.PlayClipAtPoint(_enemyFireSFX, transform.position);
        }
        yield return null;
    }
}
