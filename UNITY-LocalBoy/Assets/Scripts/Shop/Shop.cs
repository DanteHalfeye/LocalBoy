using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private ShopTier shopType = ShopTier.tier1;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        shopType = SeleccionarEnumAleatorio<ShopTier>();
        SpawnShop();
    }

    private void SpawnShop()
    {
        GameObject prefab = Resources.Load<GameObject>("Shop/Prefabs/" + shopType.ToString());
        prefab.transform.position = transform.position;
        Instantiate(prefab);
    }

    private enum ShopTier
    {
        tier1,
        tier2,
        tier3,
        tier4
    }

    private static T SeleccionarEnumAleatorio<T>() where T : Enum
    {
        T[] valores = (T[])Enum.GetValues(typeof(T));
        int indiceAleatorio = UnityEngine.Random.Range(0, valores.Length);
        return valores[indiceAleatorio];
    }
}
