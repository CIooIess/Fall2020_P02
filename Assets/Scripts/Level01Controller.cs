using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    public static Level01Controller Level01;

    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject _levelMenuPanel;
    [SerializeField] GameObject _nodesGroup;
    [SerializeField] GameObject _node;

    int _currentScore;

    private void Start()
    {
        Level01 = this;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        for (int z = -400; z < 400; z+=40)
        {
            for (int x = -400; x < 400; x+=40)
            {
                Instantiate(_node, new Vector3(x+20,0,z+20), Quaternion.Euler(0,0,0), _nodesGroup.transform);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerHealth.Player.playerHealth > 0)
        {
            _levelMenuPanel.SetActive(!_levelMenuPanel.activeSelf);

            CursorState();
        }
    }

    public void IncreaseScore(int scoreIncrease)
    {
        _currentScore += scoreIncrease;
        _currentScoreTextView.text =
            "> SCORE = " + _currentScore.ToString();
    }

    public void CursorState()
    {
        MouseLook.Lock.moveLock = !MouseLook.Lock.moveLock;

        if (_levelMenuPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            Debug.Log("Cursor Free & Visible");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            Debug.Log("Cursor Locked & Hidden");
        }
    }

    public void ExitLevel()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New High Score: " + _currentScore);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level01");
    }
}
