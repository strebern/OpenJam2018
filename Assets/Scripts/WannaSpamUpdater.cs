using System;
using UnityEngine;
using TMPro;

public class WannaSpamUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshPro _countdownText;
    [SerializeField] private TextMeshPro _scoreText;
    private float _timer = 0f;
    private bool _countdownStarted = false;

    private void Update()
    {
        if (_countdownStarted)
            _countdownText.text += "" + TimeSpan.FromSeconds(_timer);
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
