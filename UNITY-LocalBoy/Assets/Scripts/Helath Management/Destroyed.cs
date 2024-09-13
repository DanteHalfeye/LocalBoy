using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyed : MonoBehaviour
{
    DestroyedEvent destroyedEvent;

    private void Awake()
    {
        destroyedEvent = GetComponent<DestroyedEvent>();
    }

    private void OnEnable()
    {
        destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
    }
    private void OnDisable()
    {
        destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;

    }

    private void DestroyedEvent_OnDestroyed(DestroyedEvent obj)
    {
        Destroy(gameObject);
    }
}
