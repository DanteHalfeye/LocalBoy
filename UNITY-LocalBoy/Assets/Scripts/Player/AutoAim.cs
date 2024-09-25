using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    [SerializeField] float autoShootRange;
    [SerializeField] LayerMask enemyLayer;
    // Start is called before the first frame update
    public Vector2 AutoShootDirection()
    {
         Collider2D enemyToShoot = Physics2D.OverlapCircle(transform.position, autoShootRange, enemyLayer);
        
        if (enemyToShoot != null)
        {
            GameObject enemigo = enemyToShoot.gameObject;
            Vector2 shootDirection = (enemigo.transform.position - gameObject.transform.position).normalized;
            return shootDirection;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, autoShootRange);
    }
}
