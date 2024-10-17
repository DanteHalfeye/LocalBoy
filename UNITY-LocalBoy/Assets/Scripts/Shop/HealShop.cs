using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShop : MonoBehaviour
{
    [SerializeField]
    private int price;
    [SerializeField]
    private int amount;

    private GetPrice getPrice;

    private void Awake()
    {
        getPrice = transform.GetComponentInChildren<GetPrice>();
        getPrice.ShowPrice(price);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerActor actor = collision.GetComponent<PlayerActor>();
            if (actor.Currency < price) return;

            actor.Currency -= price;
            actor.ModifyCurrentHp(amount);
        }
    }
}
