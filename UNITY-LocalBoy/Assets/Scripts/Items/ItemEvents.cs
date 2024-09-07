using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemEvents
{
    public static event Action OnEnemyKilled;
    public static event Action OnRoomEntered;
    public static event Action OnStatChanged;
    public static event Action OnFloorEntered;

    // Métodos para invocar eventos
    public static void TriggerEnemyKilled()
    {
        OnEnemyKilled?.Invoke();
    }

    public static void TriggerOnStatChange()
    {
        OnStatChanged?.Invoke();
    }

    public static void TriggerOnRoomEntered()
    {
        OnRoomEntered?.Invoke();
    }

    public static void TriggerOnFloorEntered()
    {
        OnFloorEntered?.Invoke();
    }
}
