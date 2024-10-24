using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public static class ItemManager
{
    private static readonly Dictionary<PlayerActor, List<Guid>> _itemIdsByActor = new Dictionary<PlayerActor, List<Guid>>();
    private static Dictionary<(PlayerActor, ItemOrbitalSO), GameObject> instances = new Dictionary<(PlayerActor, ItemOrbitalSO), GameObject>();

    public static void RegisterItem(ItemSO item, PlayerActor actor)
    {
        ManageEffects.AddEffect(actor, item);
        TriggerEffect.Initialize(item, actor);

        if (!_itemIdsByActor.ContainsKey(actor))
        {
            _itemIdsByActor[actor] = new List<Guid>();
        }

        var itemId = item.InstanceId;

        if (!_itemIdsByActor[actor].Contains(itemId))
        {
            _itemIdsByActor[actor].Add(itemId);
        }
    }

    public static void UnregisterItem(Guid itemId, PlayerActor actor)
    {
        if (_itemIdsByActor.ContainsKey(actor) && _itemIdsByActor[actor].Contains(itemId))
        {
            _itemIdsByActor[actor].Remove(itemId);
        }
    }

    public static List<Guid> GetActiveItemIdsForActor(PlayerActor actor)
    {
        if (_itemIdsByActor.ContainsKey(actor))
        {
            return _itemIdsByActor[actor].ToList();
        }
        return new List<Guid>();
    }

    public static void RegisterInstance(PlayerActor actor, ItemOrbitalSO item, GameObject instance)
    {
        var key = (actor, item);
        if (instances.ContainsKey(key))
        {
            instances[key] = instance;
        }
        else
        {
            instances.Add(key, instance);
        }
    }

    public static GameObject GetInstance(PlayerActor actor, ItemOrbitalSO item)
    {
        var key = (actor, item);
        instances.TryGetValue(key, out GameObject instance);
        return instance;
    }

    public static void RemoveInstance(PlayerActor actor, ItemOrbitalSO item)
    {
        var key = (actor, item);
        if (instances.ContainsKey(key))
        {
            instances.Remove(key);
        }
    }
}
