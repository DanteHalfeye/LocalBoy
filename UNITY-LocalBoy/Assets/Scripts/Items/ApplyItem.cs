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
        rarityPool = GetRandomRarity<ItemRarity>();
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
            item = itemsOfSelectedRarity[UnityEngine.Random.Range(0, itemsOfSelectedRarity.Count)];
            Debug.Log(item.Name);

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

    private static T GetRandomRarity<T>() where T : Enum
    {
        // Definir los pesos en el mismo orden que los valores del enum.
        int[] weights = new int[] { 70, 25, 5 };

        // Obtener los valores del enum.
        T[] valores = (T[])Enum.GetValues(typeof(T));

        // Verificar que los pesos coincidan con el número de valores.
        if (weights.Length != valores.Length)
        {
            throw new ArgumentException("El número de pesos no coincide con el número de valores en el enum.");
        }

        // Calcular la suma total de los pesos.
        int totalWeight = 0;
        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        // Generar un número aleatorio entre 0 y el total de los pesos.
        int randomValue = UnityEngine.Random.Range(0, totalWeight);

        // Determinar qué valor corresponde al número aleatorio basado en los pesos.
        int cumulativeWeight = 0;
        for (int i = 0; i < valores.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return valores[i];
            }
        }

        throw new InvalidOperationException("No se pudo seleccionar un valor aleatorio del enum.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            text.Show(item);

            Debug.Log(item.Name);

            PlayerActor actor = other.GetComponent<PlayerActor>();

            actor.PickUpItem(item);

            Destroy(this.gameObject);



        }
    }
}
