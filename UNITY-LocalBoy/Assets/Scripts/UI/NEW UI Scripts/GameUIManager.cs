using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] RectTransform gamePanel, optionsMenu, pauseMenu;
    [SerializeField] Image darkOverlay;
    [SerializeField] CanvasGroup gamePanelCanvasGroup;
    [SerializeField] float fadeTime;

    public void pauseMenuButton()
    {
        AudioManager.PlayOneShot("UI-button", Vector3.zero);
        pauseMenu.DOAnchorPos(new Vector2(0, 0), 0f).SetUpdate(true);
        darkOverlay.DOFade(0.9f, 0.5f).SetUpdate(true);
        gamePanelCanvasGroup.interactable = false;
        Time.timeScale = 0f;
    }

    public void closePauseMenu()
    {
        AudioManager.PlayOneShot("UI-button", Vector3.zero);
        pauseMenu.DOAnchorPos(new Vector2(0, -10000), 0f).SetUpdate(true);
        darkOverlay.DOFade(0f, 0.5f).SetUpdate(true);
        gamePanelCanvasGroup.interactable = true;
        Time.timeScale = 1f;
    }
    public void OptionsMenuButton()
    {
        AudioManager.PlayOneShot("UI-button", Vector3.zero);
        optionsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f).SetUpdate(true);
    }

    public void CloseOptionsMenu()
    {
        AudioManager.PlayOneShot("UI-button", Vector3.zero);
        optionsMenu.DOAnchorPos(new Vector2(0, 5000), 0.5f).SetUpdate(true);
    }

    public void closeGamePanel()
    {
        AudioManager.PlayOneShot("UI-button", Vector3.zero);
        AudioManager.StopEmiter(RoomManager.instance.MusicFondo);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("UI New Game");
    }
}
