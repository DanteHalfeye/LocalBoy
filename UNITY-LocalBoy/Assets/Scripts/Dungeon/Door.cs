using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class Door : MonoBehaviour
{
    #region Header OBJECT REFERNCES 
    [Space(10)]
    [Header("Object Preferences")]
    #endregion
    #region Tooltip
    [Tooltip("Populate this with the BoxCollider2D component on the DoorCollider gameObject")]
    #endregion
    [SerializeField] private BoxCollider2D doorCollider;

    public bool isBoosRoomDoor= false;
    private BoxCollider2D doorTrigger;
    bool isOpen = false;
    bool previoslyOpen = false;
    Animator animator;

    private void Awake()
    {
        doorCollider.enabled = false;

        animator = GetComponent<Animator>();
        doorTrigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Settings.playerTag)
        {
            OpenDoor();
        }
    }

    private void OnEnable()
    {
        animator.SetBool(Settings.open, isOpen);
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            previoslyOpen = true;
            doorCollider.enabled = false;

            animator.SetBool(Settings.open, true);
        }
    }

    public void LockDoor()
    {
        isOpen = false;
        doorCollider.enabled = true;
        doorTrigger.enabled = false; 
        animator.SetBool(Settings.open, false);
    }

    public void UnlockDoor()
    {
        doorCollider.enabled=false;
        doorTrigger.enabled = false;
        animator.SetBool(Settings.open, true);
        if (previoslyOpen)
        {
            isOpen = false;
            OpenDoor();
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(doorCollider), doorCollider);
    }
#endif
    #endregion
}
