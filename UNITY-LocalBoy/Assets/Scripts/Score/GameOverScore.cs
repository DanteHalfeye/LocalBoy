using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    [Tooltip("Texto para mostrar el score de la partida actual")]
    [SerializeField] private TextMeshProUGUI uiCurrentScore;

    [Tooltip("Texto para mostrar el High Score")]
    [SerializeField] private TextMeshProUGUI uiHighScore;

    public void SetupGameOverScreen(float currentScore)
    {
        uiCurrentScore.text = "Your Score: " + currentScore.ToString() + " Points";

        float highScore = HighScore.instance.GetHighScore();
        uiHighScore.text = "High Score: " + highScore.ToString() + " Points";
    }

}
