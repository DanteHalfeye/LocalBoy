using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputManager inputs;
    [SerializeField]private float speed;
    private Rigidbody2D rb;
    private Vector2 movDirection;
    Vector2 oldLocation;

    //Getters and setters
    public float Speed { get { return speed; } set {  speed = value; } }

     void Awake()
    {
        inputs = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        movDirection = inputs.MoveInput;
        movDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
      Vector2 move = movDirection * speed;
      rb.velocity = move;
    }

    
}
