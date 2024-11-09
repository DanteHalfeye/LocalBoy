using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private int amount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NewHealthController actor = collision.GetComponent<NewHealthController>();
            AudioManager.PlayOneShot("pick-item", gameObject.transform.position);
            actor.Heal(amount);
            Destroy(gameObject);
        }
    }


}
