using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TriggerEffect
{
    private static Dictionary<Guid, Action> statChangedHandlers = new Dictionary<Guid, Action>();
    private static Dictionary<Guid, Action> enemyKilledHandlers = new Dictionary<Guid, Action>();
    private static Dictionary<Guid, Action> roomEnteredHandlers = new Dictionary<Guid, Action>();


    public static void Initialize(ItemSO item, PlayerActor actor)
    {
        switch (item.TriggerType)
        {
            case EffectTrigger.Once:
                EvaluateTrigger(item, actor);
                break;

            case EffectTrigger.Always:
                Action statChangedHandler = () => EvaluateTrigger(item, actor);
                statChangedHandlers[item.InstanceId] = statChangedHandler;
                ItemEvents.OnStatChanged += () => EvaluateTrigger(item, actor);
                EvaluateTrigger(item, actor);
                break;

            case EffectTrigger.OnEnemyKill:
                Action enemyKilledHandler = () => EvaluateTrigger(item, actor);
                enemyKilledHandlers[item.InstanceId] = enemyKilledHandler;
                ItemEvents.OnEnemyKilled += enemyKilledHandler;
                break;
            case EffectTrigger.OnRoomEnter:
                Action roomEnteredHandler = () => EvaluateTrigger(item, actor);
                roomEnteredHandlers[item.InstanceId] = roomEnteredHandler;
                ItemEvents.OnRoomEntered += roomEnteredHandler;
                break;
        }
    }

    public static void Unsubscribe(Guid instanceId)
    {
        if (statChangedHandlers.TryGetValue(instanceId, out Action statChangedHandler))
        {
            ItemEvents.OnStatChanged -= statChangedHandler;
            statChangedHandlers.Remove(instanceId);
        }
        else if (enemyKilledHandlers.TryGetValue(instanceId, out Action enemyKilledHandler))
        {
            ItemEvents.OnEnemyKilled -= enemyKilledHandler;
            enemyKilledHandlers.Remove(instanceId);
        }
        else if (roomEnteredHandlers.TryGetValue(instanceId, out Action roomEnteredHandler))
        {
            ItemEvents.OnRoomEntered -= roomEnteredHandler;
            roomEnteredHandlers.Remove(instanceId);
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
        Unsubscribe(item.InstanceId);
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
            foreach (var effect in item.Effects)
            {
                switch (effect.stat)
                {
                    case Stat.SetMaxHealth:
                        actor.SetMaxHp(effect.modifier);
                        break;
                    case Stat.SetCurrentHealth:
                        actor.SetHp(effect.modifier);
                        break;
                    case Stat.ModifyMaxHealth:
                        actor.ModifyMaxHp(effect.modifier);
                        break;
                    case Stat.ModifyCurrentHealth:
                        actor.ModifyCurrentHp(effect.modifier);
                        break;
                    case Stat.ModifySpeed:
                        actor.ModifyMovementSpeed(effect.modifier);
                        break;
                    case Stat.SetSpeed:
                        actor.SetMovementSpeed(effect.modifier);
                        break;
                    case Stat.SetAttackSpeed:
                        actor.SetAttackSpeed(effect.modifier);
                        break;
                    case Stat.ModifyAttackSpeed:
                        actor.ModifyAttackSpeed(effect.modifier);
                        break;
                    case Stat.ModifyMoney:
                        actor.Currency += effect.modifier;
                        break;
                    case Stat.ModifyDashSpeed:
                        actor.Currency += effect.modifier;
                        break;
                }
            }

            if (!item.CanStack)
            {
                ManageEffects.MarkEffectAsApplied(actor, item);
            }

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
        Unsubscribe(item.InstanceId);

        if (ManageEffects.HasEffectBeenApplied(actor, item))
        {
            foreach (var effect in item.Effects)
            {
                switch (effect.stat)
                {
                    case Stat.SetMaxHealth:
                        actor.SetMaxHp(-effect.modifier);
                        break;
                    case Stat.SetCurrentHealth:
                        actor.SetHp(-effect.modifier);
                        break;
                    case Stat.ModifyMaxHealth:
                        actor.ModifyMaxHp(-effect.modifier);
                        break;
                    case Stat.ModifyCurrentHealth:
                        break;
                    case Stat.ModifySpeed:
                        actor.ModifyMovementSpeed(-effect.modifier);
                        break;
                    case Stat.SetSpeed:
                        actor.SetMovementSpeed(-effect.modifier);
                        break;
                    case Stat.ModifyMoney:
                        break;
                    case Stat.ModifyDashSpeed:
                        actor.Currency -= effect.modifier;
                        break;
                }
            }
            ManageEffects.MarkEffectAsNotApplied(actor, item);
        }
    }
}