using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMoney : MonoBehaviour
{
    [SerializeField]
    private int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerActor actor = collision.GetComponent<PlayerActor>();
            actor.Currency += amount;
            Destroy(this.gameObject);
        }
    }
}
