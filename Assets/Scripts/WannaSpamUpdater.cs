using System;
using UnityEngine;
using TMPro;

public class WannaSpamUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshPro _countdownText;
    [SerializeField] private TextMeshPro _scoreText;
    private float _timer = 240;
    private bool _countdownStarted = false;

    private void Start()
    {
        _countdownStarted = true;
    }

    private void Update()
    {
        if (_countdownStarted)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                _countdownText.text = "" + (int)_timer;

            }
            else
            {
                _countdownText.fontSize = 3;
                _countdownText.text = "FILL THE SCREEN !!!";
                _countdownStarted = false;
            }
        }
    }

    // PUBLIC

    public void StartCountdown()
    {
        _countdownStarted = true;
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "" + score;
    }
}
