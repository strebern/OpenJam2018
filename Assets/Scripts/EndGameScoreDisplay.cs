using UnityEngine;
using TMPro;

public class EndGameScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    private TextMeshPro _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _scoreText.text = "" + scoreManager.Score;
        FindObjectOfType<MusicPlayer>().PlayWinMusic();
    }
}
