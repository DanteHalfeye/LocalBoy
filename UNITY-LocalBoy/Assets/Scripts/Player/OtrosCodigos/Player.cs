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
[DisallowMultipleComponent]
#endregion REQUIRE COMPONENTS



public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
   
  
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
  
   
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;


    private void Awake()
    {
        // Load components
 
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
  
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    /// <summary>
    /// Initialize the player
    /// </summary>
    public void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

    
    }

   

  

    /// <summary>
    /// Handle health changed event
    /// </summary>
    

 

    
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }


    /// <summary>
    /// Add a weapon to the player weapon dictionary
    /// </summary>
    

    
}