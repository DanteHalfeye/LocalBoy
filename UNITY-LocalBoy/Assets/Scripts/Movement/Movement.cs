using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputManager inputs;
    private float speed;
    private Rigidbody2D rb;
    private Vector2 movDirection;


    //Getters and setters
    public float Speed { get { return speed; } set {  speed = value; } }

     void Awake()
    {
        PlayerActor player = PlayerActor.Instance;
        transform.position = player.transform.position;

        Destroy(gameObject);

        inputs = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
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
