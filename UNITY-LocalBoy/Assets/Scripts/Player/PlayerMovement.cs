using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerActor actor;
    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] private bool keyboard;

    private Vector2 _input;
    private float _currentSpeed;

    public Vector2 CurrentInput { get { return _input; } set { _input = value; } }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        actor = GetComponent<PlayerActor>();
    }

    private void Update()
    {
        if (keyboard)
        {
            ProcessInput(keyboard);
        }
        else
        {
        ProcessInput();
        }
    }

    private void OnEnable()
    {
        moveActionToUse.action.Enable();
    }

    private void OnDisable()
    {
        moveActionToUse.action.Disable();
    }


    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void ProcessInput()
    {
        _input = moveActionToUse.action.ReadValue<Vector2>();
        if (_input.magnitude > 1)
            _input.Normalize();
    }
    private void ProcessInput(bool teclado)
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_input.magnitude > 1)
            _input.Normalize();
    }

    private void MoveCharacter()
    {
        if (_input != Vector2.zero)
        {
            _currentSpeed += actor.Acceleration * Time.fixedDeltaTime;
        }
        else
        {
            _currentSpeed -= actor.Deceleration * Time.fixedDeltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, actor.MaxSpeed);
        _rb.velocity = _input * _currentSpeed;
    }
}
