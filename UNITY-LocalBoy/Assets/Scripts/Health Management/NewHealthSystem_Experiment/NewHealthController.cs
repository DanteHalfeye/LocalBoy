using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using UnityEngine.Events;

public class NewHealthController : MonoBehaviour
{
    public UnityAction OnHealthChanged;
    [SerializeField] private Image _healthBarFiller;
    [SerializeField] private Transform _healthBarTransform;
    [SerializeField] private int _damageAmount, _healAmount;
    [SerializeField] private DashAttack _attack;
    private Camera _camera;
    bool isPlayerDead = false;

    private PlayerActor actor;
    private void Start()
    {
        actor = GetComponent<PlayerActor>();
    }

    public bool IsPlayerDead
    {
        get { return isPlayerDead; }
    }

    private void Awake()
    {
        actor.CurrentHealth = actor.MaxHp;
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
        actor.CurrentHealth = actor.MaxHp;
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
        actor.CurrentHealth -= damageAmount;
        actor.CurrentHealth = math.clamp(actor.CurrentHealth, 0, actor.MaxHp);

        if (actor.CurrentHealth <= 0 && !isPlayerDead) 
        {
            Die();
        }

        UpdateHealth();
        OnHealthChanged?.Invoke();
    }

    private void Heal(int healAmount)
    {
        actor.CurrentHealth += healAmount;
        actor.CurrentHealth = math.clamp(actor.CurrentHealth, 0, actor.MaxHp);
        UpdateHealth();
        OnHealthChanged?.Invoke();
    }

    private void UpdateHealth()
    {
        _healthBarFiller.fillAmount = (float)actor.HpPercent;
    }

    private void Die()
    {
        isPlayerDead = true;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }
}
