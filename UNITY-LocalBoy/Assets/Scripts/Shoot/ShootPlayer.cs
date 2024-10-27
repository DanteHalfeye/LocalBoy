using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    public float fuerza;
    [SerializeField]
    private string tagObjetivo;
    private GameObject enemy;

    public void OnShoot(Vector2 direccion, GameObject bala)
    {
        if (bala != null)
        {
            bala.transform.position = gameObject.transform.position;
            bala.GetComponent<Rigidbody2D>().velocity = direccion * fuerza;
        }
    }

    public Vector2 AutoShootDirection()
    {
        FindClosestEnemy();

        if (enemy != null)
        {
            Vector2 shootDirection = (enemy.transform.position - gameObject.transform.position).normalized;
            return shootDirection;
        }
        else
        {
            return new Vector2(1, 1);
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagObjetivo);
        float minDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject obj in enemies)
        {
            float distance = Vector2.Distance(gameObject.transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = obj;
            }
        }

        enemy = closestEnemy;
    }
}
