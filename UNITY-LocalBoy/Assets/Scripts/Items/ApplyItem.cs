using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyItem : MonoBehaviour
{
    private ItemRarity rarityPool;

    private UIText text;

    private List<ItemSO> allItems;

    private SpriteRenderer spriteRender;

    private ItemSO item;

    private void Awake()
    {
        rarityPool = GetRandomRarity();

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

        //RECORDAR HACER LA UI
        //text = DetatchFromParent.Instance.transform.Find("Joystick Canvas").Find("ItemPop").GetComponent<UIText>();
    }

    private void Start()
    {
        spriteRender.sprite = item.Icon;
    }


    private ItemRarity GetRandomRarity()
    {
        int totalWeight = 0;
        int[] weights = new int[] { 70, 25, 5 };

        for (int i = 0; i < weights.Length; i++)
        {
            totalWeight += weights[i];
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return (ItemRarity)i;
            }
        }

        return ItemRarity.Normal;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            text.Show(item);

            Debug.Log(item.Name);

            PlayerActor actor = other.GetComponent<PlayerActor>();

            //actor.PickUpItem(item);

            Destroy(gameObject);
        }
    }
}
