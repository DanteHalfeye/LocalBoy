using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateSpriteWithInput : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] float rotationSpeed = 5;

    float x, y;

    private void OnEnable()
    {
        player = transform.parent.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        float x = player.CurrentInput.x;
        float y = player.CurrentInput.y;

        if (x != 0 || y != 0)
        {
            float targetZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

            // Create a quaternion for the target rotation
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetZ);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
