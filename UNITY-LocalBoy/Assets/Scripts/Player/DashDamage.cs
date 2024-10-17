using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour
{
    CameraShake cameraShake;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float duration;
    [SerializeField]float magnitude;

        int layer;
    private void Awake()
    {
        layer = (int)Mathf.Log(enemyLayer.value, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == layer)
            {
                cameraShake = Camera.main.GetComponent<CameraShake>();
                collision.gameObject.GetComponent<EnemyDeath>().Death();
                cameraShake.StartShake(duration, magnitude);
            }
        }
    }
}
