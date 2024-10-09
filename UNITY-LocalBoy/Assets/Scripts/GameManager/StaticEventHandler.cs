using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEventHandler : MonoBehaviour
{
    public static event Action OnEntitySpawned;
    public static event Action OnEntityDied;
    public static event Action OnRoomCleared;

    public static void NotifyEntityExists()
    {
        OnEntitySpawned?.Invoke();
    }

    public static void NotifyEntityDied()
    {
        OnEntityDied?.Invoke();
    }

    public static void NotifyRoomCleared()
    {
    }
}
