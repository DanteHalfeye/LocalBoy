using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("daño al player");
            }
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
            //Debug.Log(collision.gameObject.GetComponent<Health>().health.currentHealth);
        }

        gameObject.SetActive(false);
    }
}
