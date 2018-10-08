using UnityEngine;

[CreateAssetMenu(menuName = "Score")]
public class ScoreManager : ScriptableObject
{
    public int Score = 0;
    public int MaxScoreToAdd = 50;

    public void IncreaseScore(float distance)
    {
        int scoreToAdd = (int)(1 / distance);
        Score += scoreToAdd;
    }

    public void IncreaseScore(int scoreToAdd)
    {
        Score += scoreToAdd;
    }
}
