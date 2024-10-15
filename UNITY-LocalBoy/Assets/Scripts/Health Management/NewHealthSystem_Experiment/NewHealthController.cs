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
    [SerializeField] private DashAttack _attack;
    private Camera _camera;
    bool isPlayerDead = false;

    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    public bool IsPlayerDead
    {
        get { return isPlayerDead; }
    }

    private void Awake()
    {
        _maxHealth = _currentHealth;
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        ResetHealth();
        isPlayerDead = false; 
    }

    private void Update()
    {
        _healthBarTransform.rotation = _camera.transform.rotation;
    }

    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") && !_attack.IsDashing)
        {
            TakeDamage(_damageAmount);
            collision.gameObject.SetActive(false);
            return;
        }
        if (collision.CompareTag("Heal"))
        {
            Heal(_healAmount);
            collision.gameObject.SetActive(false);
            return;
        }
    }

    private void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        _currentHealth = math.clamp(_currentHealth, 0, _maxHealth);

        // Play Damaged Audio

        if (_currentHealth <= 0 && !isPlayerDead) 
        {
            Die();
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
        //Play Death Audio
        isPlayerDead = true;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }
}
