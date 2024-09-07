using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewItem", menuName = "Item/Stat")]
public class ItemStatSO : ItemSO
{ 

    [SerializeField]
    private Stat itemStatType;
    [SerializeField]
    private int modifier;

    private void OnEnable()
    {
        ItemType = ItemTypeClass.Stats;
    }


    public Stat ItemStatType => itemStatType;
    public int Modifier => modifier;

}