using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TriggerEffect
{

    public static void Initialize(ItemSO item, PlayerActor actor)
    {
        switch (item.TriggerType)
        {
            case EffectTrigger.Always:
                EvaluateTrigger(item, actor);
                ItemEvents.OnStatChanged += () => EvaluateTrigger(item, actor);
                break;

            case EffectTrigger.OnEnemyKill:
                ItemEvents.OnEnemyKilled += () => EvaluateTrigger(item, actor);
                break;

            case EffectTrigger.OnRoomEnter:
                ItemEvents.OnRoomEntered += () => EvaluateTrigger(item, actor);
                break;

            case EffectTrigger.OnFloorEnter:
                ItemEvents.OnFloorEntered += () => EvaluateTrigger(item, actor);
                break;
        }
    }

    public static void EvaluateTrigger(ItemSO item, PlayerActor actor)
    {
        bool conditionMet = ConditionCalculation.EvaluateConditionType(item.Conditional, item.ConditionNumber, item.ConditionValue, actor);

        if (conditionMet)
        {
            ApplyItemEffect(item, actor);
        }
        else
        {
            RemoveItemEffect(item, actor);
        }
    }

    public static void ApplyItemEffect(ItemSO item, PlayerActor actor)
    {
        if (item.ItemType == ItemTypeClass.Stats)
        {
            ItemStatSO itemStat = item as ItemStatSO;
            ApplyEffectStats(itemStat, actor);
        }
        else if (item.ItemType == ItemTypeClass.Orbital)
        {
            ItemOrbitalSO itemOrbital = item as ItemOrbitalSO;
            ApplyOrbital(itemOrbital, actor);
        }
    }

    public static void RemoveItemEffect(ItemSO item, PlayerActor actor)
    {
        if (item.ItemType == ItemTypeClass.Stats)
        {
            ItemStatSO itemStat = item as ItemStatSO;
            RemoveEffectStats(itemStat, actor);
        }
        else if (item.ItemType == ItemTypeClass.Orbital)
        {
            ItemOrbitalSO itemOrbital = item as ItemOrbitalSO;
            RemoveOrbital(itemOrbital, actor);
        }
    }

    private static void ApplyOrbital(ItemOrbitalSO item, PlayerActor actor)
    {
        if (!ManageEffects.HasEffectBeenApplied(actor,item))
        {
            GameObject instance = UnityEngine.Object.Instantiate(item.Prefab, actor.transform.position, Quaternion.identity);
            instance.transform.SetParent(actor.transform);
            instance.transform.localPosition = Vector3.zero;

            ItemManager.RegisterInstance(actor, item, instance);
            ManageEffects.MarkEffectAsApplied(actor, item);

            if (item.Duration != DurationType.Infinite)
            {
                ManageItemTimers.instance.InitializeTimer(item, actor);
            }
        }
        else
        {
            ManageItemTimers.instance.IncreaseTimer(item);
        }
    }

    private static void RemoveOrbital(ItemOrbitalSO item, PlayerActor actor)
    {
        if (ManageEffects.HasEffectBeenApplied(actor, item))
        {
            GameObject Instance = ItemManager.GetInstance(actor, item);
            UnityEngine.Object.Destroy(Instance);
            ItemManager.RemoveInstance(actor, item);

            ManageEffects.MarkEffectAsNotApplied(actor, item);
        }
    }

    private static void ApplyEffectStats(ItemStatSO item, PlayerActor actor)
    {

        if (!ManageEffects.HasEffectBeenApplied(actor,item))
        {
            switch (item.ItemStatType)
            {
                case Stat.SetMaxHealth:
                    actor.SetMaxHp(item.Modifier);
                    break;
                case Stat.SetCurrentHealth:
                    actor.SetHp(item.Modifier);
                    break;
                case Stat.ModifyMaxHealth:
                    actor.ModifyMaxHp(item.Modifier); 
                    break;
                case Stat.ModifyCurrentHealth:
                    actor.ModifyCurrentHp(item.Modifier);
                    break;
                case Stat.ModifyAttackSpeed:
                    actor.ModifyAttackSpeed(item.Modifier);
                    break;
                case Stat.SetAttackSpeed:
                    actor.SetAttackSPeed(item.Modifier);
                    break;
                case Stat.AddAmmo:
                    actor.AddAmmo(item.Modifier);
                    break;
                case Stat.SetAmmo:
                    actor.SetAmmo(item.Modifier);
                    break;
                case Stat.ModifySpeed:
                    actor.ModifyMovementSpeed(item.Modifier);
                    break;
                case Stat.SetSpeed:
                    actor.SetMovementSpeed(item.Modifier);
                    break;
            }

            ManageEffects.MarkEffectAsApplied(actor, item);

            if (item.Duration != DurationType.Infinite)
            {
                ManageItemTimers.instance.InitializeTimer(item, actor);
            }

        }
        else
        {
            ManageItemTimers.instance.IncreaseTimer(item);
        }
    }

    private static void RemoveEffectStats(ItemStatSO item, PlayerActor actor)
    {
        if (ManageEffects.HasEffectBeenApplied(actor, item))
        {
            switch (item.ItemStatType)
            {
                case Stat.SetMaxHealth:
                    actor.SetMaxHp(-item.Modifier);
                    break;
                case Stat.SetCurrentHealth:
                    actor.SetHp(-item.Modifier);
                    break;
                case Stat.ModifyMaxHealth:
                    actor.ModifyMaxHp(-item.Modifier);
                    break;
                case Stat.ModifyCurrentHealth:
                    actor.ModifyCurrentHp(-item.Modifier);
                    break;
                case Stat.ModifyAttackSpeed:
                    actor.ModifyAttackSpeed(-item.Modifier);
                    break;
                case Stat.SetAttackSpeed:
                    actor.SetAttackSPeed(-item.Modifier);
                    break;
                case Stat.AddAmmo:
                    actor.AddAmmo(-item.Modifier);
                    break;
                case Stat.SetAmmo:
                    actor.SetAmmo(-item.Modifier);
                    break;
                case Stat.ModifySpeed:
                    actor.ModifyMovementSpeed(-item.Modifier);
                    break;
                case Stat.SetSpeed:
                    actor.SetMovementSpeed(-item.Modifier);
                    break;
            }
            ManageEffects.MarkEffectAsNotApplied(actor, item);
        }
    }
}