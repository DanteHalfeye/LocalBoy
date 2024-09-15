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
        // Asegurarse de que la barra verde est� al m�ximo al inicio
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Actualiza la escala de la barra verde en funci�n de la salud actual
        float healthPercentage = (float)healthComponent.health.CurrentHealth / healthComponent.health.MaxHealth;
        healthBarGreen.fillAmount = healthPercentage; // Ajusta el tama�o de la barra verde
    }
}
