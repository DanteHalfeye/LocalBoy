using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private ItemRarity rarity;


    [SerializeField]
    private Condition conditional;
    [SerializeField]
    private float conditionValue;
    [SerializeField]
    private ConditionNumberType conditionNumber;
    [SerializeField]
    private EffectTrigger triggerType;

    [SerializeField]
    private DurationType duration;
    [SerializeField]
    private float durationAmount;

    private ItemTypeClass itemType;

    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;
    public Condition Conditional => conditional;
    public float ConditionValue
    {
        get { return conditionValue; }
        set { conditionValue = value; }
    }
    public ConditionNumberType ConditionNumber => conditionNumber;
    public EffectTrigger TriggerType => triggerType;
    public DurationType Duration => duration;
    public float DurationAmount => durationAmount;
    public ItemTypeClass ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }

    public ItemRarity Rarity { get { return rarity; } }

}
