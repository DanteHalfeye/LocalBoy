using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


#region REQUIRE COMPONENTS

[RequireComponent(typeof(MovementToPositionEvent))]

[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]

[DisallowMultipleComponent]


#endregion REQUIRE COMPONENTS



public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
   
  
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
  
   
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    [HideInInspector] public IdleEvent idleEvent;


    private void Awake()
    {
        // Load components
 
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
  
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        idleEvent = GetComponent<IdleEvent>();

        
    }
    private void OnEnable()
    {
        GameManager.Instance.SetPlayer(this);
    }


    /// <summary>
    /// Initialize the player
    /// </summary>
    public void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

    
    }

   

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }




    
}