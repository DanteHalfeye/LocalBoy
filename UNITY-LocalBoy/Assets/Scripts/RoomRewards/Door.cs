using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorReward : MonoBehaviour
{
    private bool isOpen;
    [SerializeField]
    private Rewards rewardType;

    private void Start()
    {
        isOpen = false;
    }

    public void AsignReward(Rewards reward)
    {
        rewardType = reward;
    }

    private void Open()
    {
        isOpen = true;

        SpawnReward.GetReward();
    }

    public void Enter()
    {
        RoomManager.instance.CurrentReward = rewardType;
        //RECORDAR HACER EL SCENE MANAGER
        SceneManager.LoadScene("Item");
        Debug.Log("enter");

    }


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Open();
        }
    }

}

public enum Rewards
{
    Money,
    Item,
    Heal,
    Shop
}
