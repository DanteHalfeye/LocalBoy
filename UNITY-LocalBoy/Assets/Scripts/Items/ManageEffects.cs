using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class ManageEffects 
{
    private static readonly Dictionary<PlayerActor, Dictionary<Guid, bool>> _effectsAppliedByActor = new Dictionary<PlayerActor, Dictionary<Guid, bool>>();

    public static bool HasEffectBeenApplied(PlayerActor actor, ItemSO item)
    {
        if (_effectsAppliedByActor.TryGetValue(actor, out var itemEffects))
        {
            return itemEffects.TryGetValue(item.InstanceId, out var effectApplied) && effectApplied;
        }
        return false;
    }

    public static void AddEffect(PlayerActor actor, ItemSO item)
    {
        if (!_effectsAppliedByActor.ContainsKey(actor))
        {
            _effectsAppliedByActor[actor] = new Dictionary<Guid, bool>();
        }

        if (!_effectsAppliedByActor[actor].ContainsKey(item.InstanceId))
        {
            _effectsAppliedByActor[actor][item.InstanceId] = false;
        }
    }

    public static void MarkEffectAsApplied(PlayerActor actor, ItemSO item)
    {
        if (!_effectsAppliedByActor.ContainsKey(actor))
        {
            _effectsAppliedByActor[actor] = new Dictionary<Guid, bool>();
        }

        _effectsAppliedByActor[actor][item.InstanceId] = true;
    }

    public static void MarkEffectAsNotApplied(PlayerActor actor, ItemSO item)
    {
        if (!_effectsAppliedByActor.ContainsKey(actor))
        {
            _effectsAppliedByActor[actor] = new Dictionary<Guid, bool>();
        }

        _effectsAppliedByActor[actor][item.InstanceId] = false;
    }
}
