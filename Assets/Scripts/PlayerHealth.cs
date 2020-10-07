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

    void Start()
    {
        Player = this;
        healthLimit = playerHealth;
        _healthLabelViewText.text =
            "> HEALTH_STATUS = " + playerHealth.ToString();
    }

    void Update()
    {
        //for testing purposes
        if (Input.GetKeyDown(KeyCode.T))
        {
            HealthChange(-10);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            HealthChange(10);
        }
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

        _healthBarViewImage.rectTransform.localScale = new Vector3(playerHealth / healthLimit, 1f, 1f);
        _healthLabelViewText.text =
            "> HEALTH_STATUS = " + playerHealth.ToString();

        if (playerHealth <= 0)
        {
            MouseLook.Lock.moveLock = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _deathMenuPanel.SetActive(true);
        }
    }
}
