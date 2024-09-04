using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform mainMenu, gamePanel, optionsMenu, addsMenu, itemsMenu, pauseMenu, creditsPanel, gameOverPanel, rectTransform;
    [SerializeField] Image darkOverlay;
    [SerializeField] CanvasGroup canvaGroup, gamePanelCanvasGroup;
    [SerializeField] List<GameObject> items = new List<GameObject>();
 
    [SerializeField] List<RectTransform> creditItems;
    [SerializeField] float fadeTime, explosionDuration;
    [SerializeField] Ease explosionEase, fadeInEase, myEase;
    [SerializeField] Vector2 explosionRange; 

    private enum MenuState { MainMenu, PauseMenu, None }
    private MenuState previousState = MenuState.None;

    void Start()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        optionsMenu.DOAnchorPos(new Vector2(0, 850), 0.25f);
        darkOverlay.color = new Color(0, 0, 0, 0); 
    }

    public void optionsMenuButton()
    {
        optionsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }

    public void closeOptionsMenu()
    {
        optionsMenu.DOAnchorPos(new Vector2(0, 850), 0.25f);
    }

    public void gamePanelButton()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void closeGamePanel()
    {
        pauseMenu.DOAnchorPos(new Vector2(0, -850), 0f);
        darkOverlay.DOFade(0f, 0.5f);
        gamePanelCanvasGroup.interactable = true;
        gamePanel.DOAnchorPos(new Vector2(0, -1000), 0.25f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void creditsPanelButton()
    {
        gamePanel.DOAnchorPos(new Vector2(0, -1000), 0f);
        creditsPanel.DOAnchorPos(new Vector2(0, 0), 0.25f);
        StartCoroutine("CreditItemsExplosionEffect");
    }

    public void closeCreditsPanel()
    {
        creditsPanel.DOAnchorPos(new Vector2(-1800, 0), 0.25f);

        foreach (var item in creditItems)
        {
            // Establece la opacidad a 0
            CanvasGroup itemCanvasGroup = item.GetComponent<CanvasGroup>();

            if (itemCanvasGroup != null)
            {
                itemCanvasGroup.alpha = 0f;
            }

            // También puedes restablecer la escala si lo necesitas
            item.localScale = Vector3.zero;
        }

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void addsMenuButton()
    {
        addsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }

    public void closeAddsMenu()
    {
        addsMenu.DOAnchorPos(new Vector2(0, -850), 0.25f);
    }

    public void pauseMenuButton()
    {
        pauseMenu.DOAnchorPos(new Vector2(0, 0), 0f);
        darkOverlay.DOFade(0.5f, 0.5f);
        gamePanelCanvasGroup.interactable = false; 
    }

    public void closePauseMenu()
    {
        pauseMenu.DOAnchorPos(new Vector2(0, -850), 0f);
        darkOverlay.DOFade(0f, 0.5f);
        gamePanelCanvasGroup.interactable = true; 
    }

    public void itemsMenuButton()
    {
        previousState = MenuState.MainMenu; 
        mainMenu.DOAnchorPos(new Vector2(850, 0), 0.25f);
        itemsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        panelFadeIn();
    }

    public void itemsMenuButtonInGame()
    {
        previousState = MenuState.PauseMenu; // Guardar estado anterior
        pauseMenu.DOAnchorPos(new Vector2(850, 0), 0.25f);
        itemsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        panelFadeIn();
    }

    public void closeItemsMenu()
    {
        panelFadeOut();

        // Regresar al estado anterior
        switch (previousState)
        {
            case MenuState.MainMenu:
                itemsMenu.DOAnchorPos(new Vector2(2000, 0), 0.25f);
                mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
                break;
            case MenuState.PauseMenu:
                itemsMenu.DOAnchorPos(new Vector2(2000, 0), 0.25f);
                pauseMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
                break;
        }

        previousState = MenuState.None; // Resetea el estado anterior
    }

    public void gameOverButton()
    {
        gameOverPanel.DOAnchorPos(new Vector2(0, 0), 0.25f).SetEase(myEase);
        gamePanel.DOAnchorPos(new Vector2(0, -1000), 0f);
    }

    public void closeGameOverPanel()
    {
        gameOverPanel.DOAnchorPos(new Vector2(0, 1000), 0f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void panelFadeIn()
    {
        canvaGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
        canvaGroup.DOFade(1, fadeTime);
        StartCoroutine("itemsAnimation");
    }

    public void panelFadeOut()
    {
        canvaGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvaGroup.DOFade(0, fadeTime);
    }

    IEnumerator itemsAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = new Vector3(0f, 0f, 0f);
        }

        foreach (var item in items)
        {
            item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.10f);
        }
    }

    private IEnumerator CreditItemsExplosionEffect()
    {
        foreach (var item in creditItems)
        {

            // Mantener la posición original del elemento
            Vector2 originalPos = item.anchoredPosition;

            // Establecer la escala a cero y la opacidad a cero antes de la animación
            item.localScale = Vector3.zero;
            CanvasGroup itemCanvasGroup = item.GetComponent<CanvasGroup>();

            if (itemCanvasGroup == null)
            {
                itemCanvasGroup = item.gameObject.AddComponent<CanvasGroup>();
            }

            // Animar la escala desde cero a uno para simular la explosión, sin mover la posición
            item.DOScale(1f, explosionDuration).SetEase(explosionEase);

            // Aplicar el efecto de fade in simultáneamente
            itemCanvasGroup.DOFade(1f, explosionDuration).SetEase(Ease.Linear);

            // Esperar antes de "explotar" el siguiente elemento
            yield return new WaitForSeconds(0.3f);
        }
    }



    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
