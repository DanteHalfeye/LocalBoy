using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnReward
{
    public static void GetReward()
    {
        Rewards reward = RoomManager.instance.CurrentReward;

        switch (reward)
        {
            case Rewards.Heal: 
                Heal();
                break;

            case Rewards.Item:
                Item();
                break;

            case Rewards.Shop:
                Shop();
                break;

            case Rewards.Money:
                Money();
                break;
        }
    }


    private static void Heal()
    {
        Debug.Log("Heal");
    }

    private static void Item()
    {
        Debug.Log("Item");
    }

    private static void Shop()
    {
        Debug.Log("Shop");
    }

    private static void Money()
    {
        Debug.Log("Money");
    }

}
