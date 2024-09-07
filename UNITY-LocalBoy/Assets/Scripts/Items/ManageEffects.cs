using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManageEffects 
{
    private static readonly Dictionary<PlayerActor, Dictionary<ItemSO, bool>> _effectsAppliedByActor = new Dictionary<PlayerActor, Dictionary<ItemSO, bool>>();

    public static bool HasEffectBeenApplied(PlayerActor actor, ItemSO item)
    {
        if (_effectsAppliedByActor.TryGetValue(actor, out var itemEffects))
        {
            return itemEffects.TryGetValue(item, out var effectApplied) && effectApplied;
        }
        return false;
    }

    public static void AddEffect(PlayerActor actor, ItemSO item)
    {
        if (!_effectsAppliedByActor.ContainsKey(actor))
        {
            _effectsAppliedByActor[actor] = new Dictionary<ItemSO, bool>();
        }

        if (!_effectsAppliedByActor[actor].ContainsKey(item))
        {
            _effectsAppliedByActor[actor][item] = false;
        }
    }

    public static void MarkEffectAsApplied(PlayerActor actor, ItemSO item)
    {
        if (!_effectsAppliedByActor.ContainsKey(actor))
        {
            _effectsAppliedByActor[actor] = new Dictionary<ItemSO, bool>();
        }

        _effectsAppliedByActor[actor][item] = true;
    }

    public static void MarkEffectAsNotApplied(PlayerActor actor, ItemSO item)
    {
        if (!_effectsAppliedByActor.ContainsKey(actor))
        {
            _effectsAppliedByActor[actor] = new Dictionary<ItemSO, bool>();
        }

        _effectsAppliedByActor[actor][item] = false;
    }
}
