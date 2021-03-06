﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    [SerializeField] Text _highScoreTextView;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ScoreDisplay();

        if (_startingSong != null && AudioManager.Instance.playing != true)
        {
            AudioManager.Instance.PlaySong(_startingSong);
        }
    }

    void ScoreDisplay()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text =
            "> HIGH_SCORE = " + highScore.ToString();
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        ScoreDisplay();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
