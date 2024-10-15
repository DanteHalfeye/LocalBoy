using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ItemManager
{
    private static readonly Dictionary<PlayerActor, List<ItemSO>> _itemsByActor = new Dictionary<PlayerActor, List<ItemSO>>();
    private static Dictionary<(PlayerActor, ItemOrbitalSO), GameObject> instances = new Dictionary<(PlayerActor, ItemOrbitalSO), GameObject>();

    public static void RegisterItem(ItemSO item, PlayerActor actor)
    {
        ManageEffects.AddEffect(actor,item);
        TriggerEffect.Initialize(item, actor);

        if (!_itemsByActor.ContainsKey(actor))
        {
            _itemsByActor[actor] = new List<ItemSO>();
        }

        if (!_itemsByActor[actor].Contains(item))
        {
            _itemsByActor[actor].Add(item);
        }
    }

    // Elimina un item del gestor para un actor específico
    public static void UnregisterItem(ItemSO item, PlayerActor actor)
    {
        if (_itemsByActor.ContainsKey(actor) && _itemsByActor[actor].Contains(item))
        {
            _itemsByActor[actor].Remove(item);
        }
    }

    // Obtiene todos los items activos para un actor específico
    public static IEnumerable<ItemSO> GetActiveItemsForActor(PlayerActor actor)
    {
        if (_itemsByActor.ContainsKey(actor))
        {
            return _itemsByActor[actor];
        }
        return Enumerable.Empty<ItemSO>();
    }

    public static void RegisterInstance(PlayerActor actor, ItemOrbitalSO item, GameObject instance)
    {
        var key = (actor, item);
        if (instances.ContainsKey(key))
        {
            // Actualizar si ya existe
            instances[key] = instance;
        }
        else
        {
            // Añadir nueva entrada
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
