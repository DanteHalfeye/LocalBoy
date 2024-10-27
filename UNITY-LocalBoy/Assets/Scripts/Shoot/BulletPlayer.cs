using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] float autoDisableTimer;
    float timer;
    bool recienDisparada = true;
    [SerializeField]
    private LayerMask tagObjetivo;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == tagObjetivo)
            {
                collision.gameObject.GetComponent<EnemyDeath>().Death();
            }
        }
    }
}
