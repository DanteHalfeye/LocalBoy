using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] RectTransform gameOverPanel, scorePanel;
    [SerializeField] Ease myEase;

    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    void Start()
    {
        gameOverPanel.DOAnchorPos(new Vector2(0, 0), 0.25f).SetEase(myEase);
    }

    public void ScoreResultsPanel()
    {
        gameOverPanel.DOAnchorPos(new Vector2(0, 10000), 0f);
        scorePanel.DOAnchorPos(new Vector2(0, 0), 0.25f).SetEase(myEase);

        float finalScore = Counter.instance.GetFinalScore();
        float highScore = HighScore.instance.GetHighScore();

        currentScoreText.text = "Score: " + finalScore.ToString("F0") + " Points";
        highScoreText.text = "High Score: " + highScore.ToString("F0") + " Points";
    }

    public void CloseGameOverPanel()
    {
        scorePanel.DOAnchorPos(new Vector2(0, 11000), 0.25f).SetEase(myEase);
        SceneManager.LoadScene("UI New Game", LoadSceneMode.Single);
    }


}
