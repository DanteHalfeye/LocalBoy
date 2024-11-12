using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyItem_noRNG_shop : MonoBehaviour
{
    [SerializeField]
    private ItemRarity rarityPool;
    private GetPrice getPrice;
    private int price;


    private UIText text;

    private List<ItemSO> allItems;

    private SpriteRenderer spriteRender;

    private ItemSO item;

    private void OnEnable()
    {
        getPrice = transform.GetComponentInChildren<GetPrice>();
        price = (int)rarityPool;
        getPrice.ShowPrice(price);

        spriteRender = GetComponent<SpriteRenderer>();

        allItems = new List<ItemSO>(Resources.LoadAll<ItemSO>("Items"));

        List<ItemSO> itemsOfSelectedRarity = new List<ItemSO>();

        foreach (ItemSO item in allItems)
        {
            if (item.Rarity == rarityPool)
            {
                itemsOfSelectedRarity.Add(item);
            }
        }

        if (itemsOfSelectedRarity.Count > 0)
        {
            item = itemsOfSelectedRarity[UnityEngine.Random.Range(0, itemsOfSelectedRarity.Count)];

        }
        else
        {
            Debug.Log("No hay objetos con la rareza seleccionada.");
        }

        text = UIText.instance;
    }

    private void Start()
    {
        spriteRender.sprite = item.Icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerActor actor = other.GetComponent<PlayerActor>();
            if (actor.Currency < price) return;
            text.Show(item);

            AudioManager.PlayOneShot("pick-item", gameObject.transform.position);
            item.Initialize();
            if (!item.CanStack)
            {
                ItemPool.RemoveItemFromPool(item);
            }


            actor.Currency -= price;

            actor.PickUpItem(item);

            Destroy(gameObject);
        }
    }
}
