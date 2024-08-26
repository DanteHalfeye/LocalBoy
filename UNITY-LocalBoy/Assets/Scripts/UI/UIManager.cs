using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform mainMenu, gamePanel, optionsMenu, addsMenu, itemsMenu;
    public float fadeTime = 1f;
    public CanvasGroup canvaGroup;
    public RectTransform rectTransform, colorTransform;
    public Ease myEase;
    public List<GameObject> items = new List<GameObject>();

    //public Image panelColor;
    //public Color endColor;
    void Start()
    {
        mainMenu.DOAnchorPos(new Vector2(0,0), 0.25f);
    }

    public void optionsMenuButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-850, 0), 0.25f);
        optionsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }

    public void closeOptionsMenu()
    {
        optionsMenu.DOAnchorPos(new Vector2(0, 850), 0.25f);
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }

    public void gamePanelButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0,-1000), 0.25f);
        gamePanel.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(myEase);
    }

    public void closeGamePanel()
    {
        optionsMenu.DOAnchorPos(new Vector2(0, -1000), 0.25f);
        gamePanel.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }

    public void addsMenuButton()
    {
        addsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        //colorTransform.DOColor(endColor, 0.5f).SetDelay(0.7f);
    }

    public void closeAddsMenu()
    {
        addsMenu.DOAnchorPos(new Vector2(0, -850), 0.25f);
    }

    public void itemsMenuButton()
    {
        mainMenu.DOAnchorPos(new Vector2(850, 0), 0.25f);
        itemsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        panelFadeIn();
    }

    public void closeItemsMenu()
    {
        panelFadeOut();
        itemsMenu.DOAnchorPos(new Vector2(850, 0), 0.25f);
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
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
        foreach(var item in items)
        {
            item.transform.localScale = new Vector3(0f, 0f, 0f);
        }

        foreach(var item in items)
        {
            item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.10f);
        }
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
