using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Counter : MonoBehaviour
{
    [SerializeField]
    private bool debug;

    [SerializeField]
    private int decreaseIndex;

    [SerializeField]
    public float currentScore; //lo cambio a public por ahora, para poder referenciarlo desde el DetectEventos

    [SerializeField]
    private float maxTimer;

    [SerializeField]
    private float multiplierTimer;
    [SerializeField]
    private float decreaseMultiplierTimer;


    private bool isTiming;

    [SerializeField]
    private CurrentMultiplier currentMultiplier;
    private CurrentMultiplier[] enumIndex;

    private TextMeshProUGUI uiScore;
    private TextMeshProUGUI uiMultiplier; 

    private void Awake()
    {
        enumIndex = (CurrentMultiplier[])System.Enum.GetValues(typeof(CurrentMultiplier));
        uiScore = GetComponent<TextMeshProUGUI>();
        uiMultiplier = transform.GetComponentInChildren<TextMeshProUGUI>();
        currentMultiplier = CurrentMultiplier.soyJak;
        isTiming = false;
    }

    private void Update()
    {
        uiScore.text = "Score: " + currentScore.ToString();
        uiMultiplier.text = currentMultiplier.ToString();

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            AddScore(20, debug);
        }
    }

    public void AddScore(float score, bool epico)
    {
        currentScore += (score * (int)currentMultiplier);

        if (epico)
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