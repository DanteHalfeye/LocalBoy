using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ItemPool
{
    private static List<ItemSO> itemPool = new List<ItemSO>();


    public static void LoadPool()
    {
        itemPool = new List<ItemSO>(Resources.LoadAll<ItemSO>("Items"));
    }

    public static ItemSO GetItemFromPool()
    {
        ItemRarity rarity;
        ItemSO choosedItem;
        rarity = GetRandomRarity<ItemRarity>();
        List<ItemSO> rarityPool = new List<ItemSO>();


        foreach (ItemSO item in itemPool)
        {
            if (item.Rarity == rarity)
            {
                rarityPool.Add(item);
            }
        }

        if (rarityPool.Count > 0)
        {
            choosedItem = rarityPool[UnityEngine.Random.Range(0, rarityPool.Count)];
            return choosedItem;
        }
        else
        {
            Debug.Log("No hay objetos con la rareza seleccionada.");
            return null;
        }

    }

    public static void RemoveItemFromPool(ItemSO itemToRemove)
    {
        if (itemPool.Contains(itemToRemove))
        {
            itemPool.Remove(itemToRemove);
            Debug.Log("Item removed from pool: " + itemToRemove.name);
        }
        else
        {
            Debug.LogWarning("Item not found in pool: " + itemToRemove.name);
        }
    }

    private static T GetRandomRarity<T>() where T : Enum
    {
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
}
