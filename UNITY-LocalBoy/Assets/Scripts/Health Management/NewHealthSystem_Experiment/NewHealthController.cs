using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
public class NewHealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealth, _currentHealth;
    [SerializeField] private Image _healthBarFiller;
    [SerializeField] private Transform _healthBarTransform;
    [SerializeField] private int _damageAmount, _healAmount;
    private Camera _camera;

    private void Awake()
    {
        _maxHealth = _currentHealth;
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    private void Update()
    {
        _healthBarTransform.rotation = _camera.transform.rotation;
    }

    private void ResetHealth()
    {
        _maxHealth = _currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            TakeDamage(_damageAmount);
        }
        else if (collision.CompareTag("Heal"))
        {
            Heal(_healAmount);
            collision.gameObject.SetActive(false);
        }
    }
    private void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        _currentHealth = math.clamp(_currentHealth, 0, _maxHealth);
        if (_currentHealth <= 0)
        {
            Die();
            _maxHealth = _currentHealth;
        }
        UpdateHealth();
    }

    private void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        _currentHealth = math.clamp(_currentHealth, 0, _maxHealth);
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        _healthBarFiller.fillAmount = (float)_currentHealth / _maxHealth;
    }

    private void Die()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }
}
