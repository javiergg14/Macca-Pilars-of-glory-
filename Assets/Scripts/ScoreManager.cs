using UnityEngine;
using TMPro; // Necesario si usas TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Instancia estática para acceso global
    public int score = 0; // Puntuación actual
    public TextMeshProUGUI scoreText; // Referencia al texto de la UI

    void Awake()
    {
        // Asegura que solo haya una instancia del ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Inicializa el texto de la puntuación
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void SubtractScore(int points)
    {
        score -= points;
        UpdateScoreText();
    }

    public int CheckScore()
    {
        return score;
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }
        else
        {
            Debug.LogWarning("No se ha asignado un componente de texto para mostrar la puntuación.");
        }
    }
}
