using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputManager inputs;
    [SerializeField]private float speed;
    private Rigidbody2D rb;
    private Vector2 movDirection;


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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movDirection * (speed/32) );
    }
}
