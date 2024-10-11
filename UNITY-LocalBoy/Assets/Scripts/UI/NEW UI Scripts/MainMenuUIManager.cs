using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] RectTransform mainMenu, optionsMenu, addsMenu, creditsPanel, creditsBackground;
    [SerializeField] List<RectTransform> creditItems;
    [SerializeField] float explosionDuration;
    [SerializeField] Ease explosionEase;
    [SerializeField] Vector2 explosionRange;

    public void GamePanelButton()
    {
        SceneManager.LoadScene("MAIN GAME", LoadSceneMode.Single);
    }
    public void OptionsMenuButton()
    {
        optionsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f).SetUpdate(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.DOAnchorPos(new Vector2(0, 5000), 0.5f).SetUpdate(true);
    }
    public void CreditsPanelButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, -10000), 0f);
        creditsBackground.DOAnchorPos(new Vector2(0, 0), 0.25f);
        creditsPanel.DOAnchorPos(new Vector2(0, 0), 0.25f);
        StartCoroutine(CreditItemsExplosionEffect());
    }

    public void CloseCreditsPanel()
    {
        creditsBackground.DOAnchorPos(new Vector2(-5000, 0), 0.25f);
        creditsPanel.DOAnchorPos(new Vector2(-5000, 0), 0.25f);

        foreach (var item in creditItems)
        {
            CanvasGroup itemCanvasGroup = item.GetComponent<CanvasGroup>();

            if (itemCanvasGroup != null)
            {
                itemCanvasGroup.alpha = 0f;
            }

            item.localScale = Vector3.zero;
        }
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0f);
    }
    public void AddsMenuButton()
    {
        addsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f).SetUpdate(true);
    }

    public void CloseAddsMenu()
    {
        addsMenu.DOAnchorPos(new Vector2(0, -5000), 0.5f).SetUpdate(true);
    }

    private IEnumerator CreditItemsExplosionEffect()
    {
        foreach (var item in creditItems)
        {
            Vector2 originalPos = item.anchoredPosition;

            item.localScale = Vector3.zero;
            CanvasGroup itemCanvasGroup = item.GetComponent<CanvasGroup>();

            if (itemCanvasGroup == null)
            {
                itemCanvasGroup = item.gameObject.AddComponent<CanvasGroup>();
            }

            item.DOScale(1f, explosionDuration).SetEase(explosionEase);

            itemCanvasGroup.DOFade(1f, explosionDuration).SetEase(Ease.Linear);

            yield return new WaitForSecondsRealtime(0.25f);
        }
    }
}
