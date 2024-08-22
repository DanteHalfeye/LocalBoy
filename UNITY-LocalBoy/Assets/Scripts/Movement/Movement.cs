using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputManager inputs;
    public float speed;
    private Rigidbody2D rb;
    private Vector2 movDirection;

    private void Awake()
    {
        inputs = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        movDirection = inputs.MoveInput.normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movDirection * speed * Time.fixedDeltaTime);
    }
}
