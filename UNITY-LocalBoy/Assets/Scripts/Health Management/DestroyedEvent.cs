using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedEvent : MonoBehaviour
{
    public event Action<DestroyedEvent> OnDestroyed;

    public void CallDestroyedEvent()
    {
        OnDestroyed?.Invoke(this);
    }
}
