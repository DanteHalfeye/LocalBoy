using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private float delayBeforeDeactivation = 1f; // Delay to allow particles to play

    EnemyShoot enemyShoot;
    PatrullaPuntos patrullaPuntos;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        enemyShoot = GetComponent<EnemyShoot>();
        patrullaPuntos = GetComponent<PatrullaPuntos>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StaticEventHandler.NotifyEntityExists();
    }
    private void OnDisable()
    {
        StaticEventHandler.NotifyEntityDied();
    }
    public void Death()
    {
        // Play the particle effect
        _particles.Play();
        enemyShoot.enabled = false;
        patrullaPuntos.enabled = false;
        spriteRenderer.enabled = false;

        // Start the coroutine to deactivate the enemy after the delay
        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        // Wait for the duration of the particle effect (or a custom delay)
        yield return new WaitForSeconds(delayBeforeDeactivation);

        // Deactivate the enemy object
        gameObject.SetActive(false);
    }
}
