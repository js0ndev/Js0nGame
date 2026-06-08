using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Pontuação: " + score;
    }
}
