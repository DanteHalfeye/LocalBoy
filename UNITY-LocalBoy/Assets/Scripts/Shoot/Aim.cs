using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private InputManager inputs;

    private Vector2 aimDirection;
    public GameObject shootPreview;


    public float calibracion;


    private void Awake()
    {
        inputs = GetComponent<InputManager>();
    }

    private void Update()
    {
        aimDirection = inputs.FireInput.normalized;

        Aiming();

        
    }

    public void Aiming()
    {
        if (aimDirection != Vector2.zero && inputs.FireInput.magnitude > 0.75f)
        {
            shootPreview.SetActive(true);

            float angle = (Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg) + calibracion;
            shootPreview.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if(shootPreview.activeSelf) 
        {
            shootPreview.SetActive(false);
        }
    }
}
