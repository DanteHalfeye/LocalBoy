using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveActionToUse, buttonPress;

    private Rigidbody2D _rb;
    [SerializeField] private float _acceleration = 0.5f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _maxSpeed = 5f;
    private Vector2 _input;
    private float _currentSpeed;

    public Vector2 CurrentInput { get { return _input; } set { _input = value; } }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInput();
        if (buttonPress.action.WasPressedThisFrame())
        {
            Button();
            
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void ProcessInput()
    {
        Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>().normalized;
        print(moveActionToUse.action.ReadValue<Vector2>().normalized);
        if (moveDirection.magnitude == 0)
        {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            _input = moveDirection;
        }
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
    public void Button()
    {
        Debug.Log("Presionando botón");
    }

    
}
