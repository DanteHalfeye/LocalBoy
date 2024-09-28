using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageItemTimers : MonoBehaviour
{
    /*
    public static ManageItemTimers instance {  get; private set; }
    private Dictionary<ItemSO, float> activeTimers = new Dictionary<ItemSO, float>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeTimer(ItemSO item, PlayerActor actor)
    {
        if (item.Duration == DurationType.Time)
        {
            StartCoroutine(HandleTimer(item, actor));
        }
        else if (item.Duration == DurationType.Rooms) 
        {
            StartCoroutine(HandleRooms(item, actor));
        }
        else if (item.Duration == DurationType.Floor)
        {
            StartCoroutine(HandleFloors(item, actor));
        }

    }

    private IEnumerator HandleTimer(ItemSO item, PlayerActor actor) 
    {
        activeTimers[item] = item.DurationAmount;

        while (activeTimers[item] > 0)
        {
            activeTimers[item] -= Time.deltaTime;
            yield return null;
        }

        TriggerEffect.RemoveItemEffect(item, actor);
        activeTimers.Remove(item);

    }

    private IEnumerator HandleRooms(ItemSO item, PlayerActor actor)
    {
        float count = 0;

        void IncrementRoomCount()
        {
            count++;
        }

        ItemEvents.OnRoomEntered += IncrementRoomCount;

        while (count >= item.DurationAmount)
        {
            yield return null;
        }

        ItemEvents.OnRoomEntered -= IncrementRoomCount;

        TriggerEffect.RemoveItemEffect(item, actor);
    }

    private IEnumerator HandleFloors(ItemSO item, PlayerActor actor)
    {
        float count = 0;

        void IncrementFloorCount()
        {
            count++;
        }

        ItemEvents.OnRoomEntered += IncrementFloorCount;

        while (count >= item.DurationAmount)
        {
            yield return null;
        }

        ItemEvents.OnRoomEntered -= IncrementFloorCount;

        TriggerEffect.RemoveItemEffect(item, actor);
    }

    public void IncreaseTimer(ItemSO item)
    {
        if(activeTimers.ContainsKey(item))
        {
            activeTimers[item] += item.DurationAmount;
        }
    }
    */
}

