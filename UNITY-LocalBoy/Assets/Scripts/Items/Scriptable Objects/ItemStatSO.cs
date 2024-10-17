using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewItem", menuName = "Item/Stat")]
public class ItemStatSO : ItemSO
{
    [SerializeField]
    private List<StatEffect> effects = new List<StatEffect>();

    private void OnEnable()
    {
        ItemType = ItemTypeClass.Stats;
    }

    public IReadOnlyList<StatEffect> Effects => effects.AsReadOnly();

    [System.Serializable]
    public struct StatEffect
    {
        public Stat stat;
        public int modifier;
    }
}