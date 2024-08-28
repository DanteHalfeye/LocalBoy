using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform mainMenu, gamePanel, optionsMenu, addsMenu, itemsMenu, pauseMenu, creditsPanel, gameOverPanel;
    [SerializeField] Image darkOverlay;
    [SerializeField] CanvasGroup gamePanelCanvasGroup;
    public RectTransform rectTransform;
    public CanvasGroup canvaGroup;
    public List<GameObject> items = new List<GameObject>();

    [SerializeField] List<RectTransform> creditItems; 
    public float fadeTime = 1f;
    public float explosionDuration = 0.5f; 
    public Vector2 explosionRange = new Vector2(100f, 100f); 
    public Ease explosionEase = Ease.OutBack; 
    public Ease fadeInEase = Ease.InQuad; 
    public Ease myEase;

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
        mainMenu.DOAnchorPos(new Vector2(0, -1000), 0.25f);
        gamePanel.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(myEase);
    }

    public void closeGamePanel()
    {
        pauseMenu.DOAnchorPos(new Vector2(0, -850), 0f);
        darkOverlay.DOFade(0f, 0.5f);
        gamePanelCanvasGroup.interactable = true;
        gamePanel.DOAnchorPos(new Vector2(0, -1000), 0.25f);
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
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

            // Tambi�n puedes restablecer la escala si lo necesitas
            item.localScale = Vector3.zero;
        }

        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
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
                itemsMenu.DOAnchorPos(new Vector2(850, 0), 0.25f);
                mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
                break;
            case MenuState.PauseMenu:
                itemsMenu.DOAnchorPos(new Vector2(850, 0), 0.25f);
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
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0f);
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

            // Mantener la posici�n original del elemento
            Vector2 originalPos = item.anchoredPosition;

            // Establecer la escala a cero y la opacidad a cero antes de la animaci�n
            item.localScale = Vector3.zero;
            CanvasGroup itemCanvasGroup = item.GetComponent<CanvasGroup>();

            if (itemCanvasGroup == null)
            {
                itemCanvasGroup = item.gameObject.AddComponent<CanvasGroup>();
            }

            // Animar la escala desde cero a uno para simular la explosi�n, sin mover la posici�n
            item.DOScale(1f, explosionDuration).SetEase(explosionEase);

            // Aplicar el efecto de fade in simult�neamente
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
