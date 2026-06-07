using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Inimigo")]
    [SerializeField] private int pontuacao = 100;
    
    public void Die()
    {
        ScoreManager.Instance.AddScore(pontuacao);
        Destroy(gameObject);
    }
}
