using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Counter : MonoBehaviour
{
    [Tooltip("Que tanto decrecera el multiplier del score cuando el timer se acaba")]
    [SerializeField]
    private int decreaseIndex;
    private float currentScore;

    [Tooltip("Timer maximo del multiplier")]
    [SerializeField]
    private float maxTimer;
    private float multiplierTimer;
    [Tooltip("Que tan rapido decrece el timer")]
    [SerializeField]
    private float decreaseMultiplierTimer;

    private bool isTiming;

    private CurrentMultiplier currentMultiplier;
    private CurrentMultiplier[] enumIndex;

    private TextMeshProUGUI uiScore;
    [Tooltip("Texto del multiplicador")]
    [SerializeField]
    private TextMeshProUGUI uiMultiplier;

    [SerializeField]
    private UnityEngine.UI.Slider slider;

    public static Counter instance;



    private void Awake()
    {
        slider.value = 0;
        enumIndex = (CurrentMultiplier[])System.Enum.GetValues(typeof(CurrentMultiplier));
        uiScore = GetComponent<TextMeshProUGUI>();
        currentMultiplier = CurrentMultiplier.soyJak;
        isTiming = false;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

    }

    private void Update()
    {
        uiScore.text = "Score: " + currentScore.ToString();
        uiMultiplier.text = "G Force : " + ((int)currentMultiplier).ToString();
        slider.value = Mathf.Clamp(multiplierTimer / maxTimer, 0f, 1f);


        
    }

    public void AddScore(float score, bool epico)
    {
        Debug.Log("score:" + currentScore);

        currentScore += (score * (int)currentMultiplier);

        if (epico && currentMultiplier != CurrentMultiplier.basado)
        {
            ChangeMultiplier(CambiarIndex(1));
        }

        if (isTiming == true)
        {
            multiplierTimer = maxTimer;
        }
        else
        {
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        isTiming = true;
        multiplierTimer = maxTimer;

        while (multiplierTimer > 0)
        {
            multiplierTimer -= (decreaseMultiplierTimer * Time.deltaTime);
            yield return null;
        }
        
        isTiming = false;

        if (currentMultiplier != CurrentMultiplier.soyJak)
        {
            ChangeMultiplier(CambiarIndex(decreaseIndex));
            StartCoroutine(Timer());
        }
    }

    private void ChangeMultiplier(CurrentMultiplier multiplier)
    {
        currentMultiplier = multiplier;
    }

    private CurrentMultiplier CambiarIndex(int index)
    {
        int currentIndex = Array.IndexOf(enumIndex, currentMultiplier);

        int newIndex = (currentIndex + index + enumIndex.Length) % enumIndex.Length;

        // Retornamos el valor correspondiente al nuevo índice
        return enumIndex[newIndex];
    }
}

enum CurrentMultiplier
{
    soyJak = 1,
    cringe = 2,
    nomas = 3,
    basado = 4,
    god = 5
}