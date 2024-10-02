using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] RectTransform gameOverPanel;
    [SerializeField] Ease myEase;
    void Start()
    {
        gameOverPanel.DOAnchorPos(new Vector2(0, 0), 0.25f).SetEase(myEase);
    }

    public void closeGameOverPanel()
    {
        gameOverPanel.DOAnchorPos(new Vector2(0, 10000), 0f);
        SceneManager.LoadScene("UI New Game", LoadSceneMode.Single);
    }


}
