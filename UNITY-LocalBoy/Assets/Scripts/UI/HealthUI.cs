using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health healthComponent;   
    public Image healthBarGreen;      
    public Image healthBarRed;        

    void Start()
    {
        UpdateHealthBar();

        healthComponent.health.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = (float)healthComponent.health.CurrentHealth / healthComponent.health.MaxHealth;
        healthBarGreen.fillAmount = healthPercentage;
    }

    private void OnDestroy()
    {
        healthComponent.health.OnHealthChanged -= UpdateHealthBar;
    }
}
