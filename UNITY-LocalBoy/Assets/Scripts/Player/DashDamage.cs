using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
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
                collision.gameObject.SetActive(false);
            }
        }
    }
}
