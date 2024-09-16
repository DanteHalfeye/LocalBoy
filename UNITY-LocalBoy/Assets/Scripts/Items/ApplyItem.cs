using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyItem : MonoBehaviour
{
    [SerializeField]
    private ItemRarity rarityPool;

    private UIText text;

    private List<ItemSO> allItems;

    private SpriteRenderer spriteRender;

    private ItemSO item;

    private void Awake()
    {
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

        // Escoger uno aleatoriamente
        if (itemsOfSelectedRarity.Count > 0)
        {
            item = itemsOfSelectedRarity[Random.Range(0, itemsOfSelectedRarity.Count)];

        }
        else
        {
            Debug.Log("No hay objetos con la rareza seleccionada.");
        }


        text = DetatchFromParent.Instance.transform.Find("Joystick Canvas").Find("ItemPop").GetComponent<UIText>();
    }

    private void Start()
    {
        spriteRender.sprite = item.Icon;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            text.Show(item);

            Debug.Log(item.Name);

            PlayerActor actor = other.GetComponent<PlayerActor>();

            actor.PickUpItem(item);

            Destroy(gameObject);



        }
    }
}
