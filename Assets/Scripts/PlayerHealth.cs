using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Player;

    [SerializeField] GameObject _deathMenuPanel;
    [SerializeField] Image _healthBarViewImage;
    [SerializeField] Text _healthLabelViewText;

    public float playerHealth = 100f;
    float healthLimit;

    [Space(10)]
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] AudioClip _healSound;
    [SerializeField] AudioClip _deathSound;

    void Start()
    {
        Player = this;
        healthLimit = playerHealth;
        _healthLabelViewText.text =
            "> HEALTH_STATUS = " + playerHealth.ToString();
    }

    public void HealthChange(int changeAmount)
    {
        if (playerHealth > 0)
        {
            playerHealth += changeAmount;
        }
        if (playerHealth > healthLimit)
        {
            playerHealth = healthLimit;
        }
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }

        if (changeAmount < 0)
            AudioManager.Instance.PlaySFX(_hurtSound, 2);
        if (changeAmount > 0)
            AudioManager.Instance.PlaySFX(_healSound, 2);

        _healthBarViewImage.rectTransform.localScale = new Vector3(playerHealth / healthLimit, 1f, 1f);
        _healthLabelViewText.text =
            "> HEALTH_STATUS = " + playerHealth.ToString();

        if (playerHealth <= 0)
        {
            MouseLook.Lock.moveLock = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AudioManager.Instance.PlaySFX(_deathSound, 2);
            Time.timeScale = 0;

            _deathMenuPanel.SetActive(true);
        }
    }
}
