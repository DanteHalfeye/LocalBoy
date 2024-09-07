using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New Item", menuName = "Item/Orbital")]
public class ItemOrbitalSO : ItemSO
{
    [SerializeField]
    private GameObject prefab;


    private void OnEnable()
    {
        ItemType = ItemTypeClass.Orbital;
    }

    public GameObject Prefab
    { 
        get { return prefab; } 
    }



}
