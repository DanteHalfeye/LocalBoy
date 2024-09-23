using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _acceleration = 0.5f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _maxSpeed = 5f;
    private Vector2 _input;
    private float _currentSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInput();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void ProcessInput()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_input.magnitude > 1)
            _input.Normalize();
    }

    private void MoveCharacter()
    {
        if (_input != Vector2.zero)
        {
            _currentSpeed += _acceleration * Time.fixedDeltaTime;
        }
        else
        {
            _currentSpeed -= _deceleration * Time.fixedDeltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
        _rb.velocity = _input * _currentSpeed;
    }
}
