using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthStatsSO health;
    public HealthUI healthUI;  // Referencia a la UI

    public void Heal(int amount)
    {
        health.Heal(amount);
        healthUI.UpdateHealthBar();  // Actualiza la barra verde
    }

    public void TakeDamage(int amount)
    {
        health.TakeDamage(amount);
        healthUI.UpdateHealthBar();  // Actualiza la barra verde

        if (health.CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Manejar la muerte (como desactivar el objeto o hacer algo más)
        if (GetComponent<DestroyedEvent>() == null)
        {
            gameObject.SetActive(false);
        }
        DestroyedEvent destroyed = GetComponent<DestroyedEvent>();
        destroyed.CallDestroyedEvent();
        Debug.Log("morí");
    }

}
