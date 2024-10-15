using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(AutoAim))]
public class DashAttack : MonoBehaviour
{
    [SerializeField] private float _dashSpeed = 20f;  // Speed during dash
    [SerializeField] private float _dashDuration = 0.2f;  // How long the dash lasts
    [SerializeField] private float _dashCooldown = 1f;  // Cooldown between dashes
    [SerializeField] private InputActionReference dashActionReference;

    private bool _isDashing = false;
    private bool _canDash = true;

    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb;
    private AutoAim autoAimDirection;

    [SerializeField]private CircleCollider2D _attackCollider;
    private BoxCollider2D _playerCollider;
    public bool IsDashing { get { return _isDashing; } }
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();
        _attackCollider = GetComponentInChildren<CircleCollider2D>();
        _playerCollider = GetComponent<BoxCollider2D>();
        autoAimDirection = GetComponent<AutoAim>();

        DeactivateAttackHitbox();
    }

    private void OnEnable()
    {
        dashActionReference.action.Enable();
    }

    private void OnDisable()
    {
        dashActionReference.action.Disable();
    }

    private void Update()
    {
        if (dashActionReference.action.WasPressedThisFrame() && _canDash)  // Press space to dash
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        // Deactivate movement for the duration of the dash
        _canDash = false;
        _isDashing = true;
        _playerMovement.enabled = false;  // Disable player movement

        //Play Audio Once Change Pitch

        ActivateAttackHitbox();
        Vector2 direction = autoAimDirection.AutoShootDirection().normalized;

        // Dash in the direction of movement
        if (direction == Vector2.zero)
        {
            direction = _playerMovement.CurrentInput;
        }

        _rb.velocity = direction * _dashSpeed;
        // Wait for dash duration
        yield return new WaitForSeconds(_dashDuration);

        DeactivateAttackHitbox();
        ReactivatePlayerMovement();

        // Cooldown before next dash
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    private void ActivateAttackHitbox()
    {
        _attackCollider .enabled = true;
        Debug.DrawLine(transform.position - Vector3.one * 1.5f, transform.position + Vector3.one * 1.5f, Color.red, 0.1f); // Runtime debug line for hitbox visibility
        
    }

    private void DeactivateAttackHitbox()
    {
        _attackCollider.enabled = false;
    }


    // Reactivate player movement
    private void ReactivatePlayerMovement()
    {
        _rb.velocity = Vector2.zero;  // Reset velocity after dash
        _playerMovement.enabled = true;  // Re-enable player movement
        _isDashing = false;
    }
    private void OnDrawGizmos()
    {
        if (IsDashing)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, Vector3.one * 3); // Visualization in editor
        }
    }
}
