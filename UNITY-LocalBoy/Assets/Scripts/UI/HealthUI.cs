using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health healthComponent;    // Referencia al componente de salud
    public Image healthBarGreen;      // Imagen que representa la vida (la barra verde)
    public Image healthBarRed;        // Imagen de fondo (la barra roja)

    void Start()
    {
        // Asegurarse de que la barra verde está al máximo al inicio
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Actualiza la escala de la barra verde en función de la salud actual
        float healthPercentage = (float)healthComponent.health.CurrentHealth / healthComponent.health.MaxHealth;
        healthBarGreen.fillAmount = healthPercentage; // Ajusta el tamaño de la barra verde
    }
}
