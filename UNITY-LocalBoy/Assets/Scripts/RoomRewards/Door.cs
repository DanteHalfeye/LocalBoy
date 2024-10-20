using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorReward : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private Rewards rewardType;
    private GameObject hint;

    private void Awake()
    {
        hint = transform.GetChild(0).gameObject;
        hint.SetActive(true);
    }

    public void AsignReward(Rewards reward)
    {
        rewardType = reward;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Enter();
        }
    }

    public void Enter()
    {
        AudioManager.PlayOneShot("new-level", gameObject.transform.position);
        RoomManager.instance.CurrentReward = rewardType;
        SceneManager.LoadScene(sceneToLoad);
        ItemEvents.TriggerOnRoomEntered();
    }


    public Rewards RewardType
    {
        get { return rewardType; }
    }
}

public enum Rewards
{
    Money,
    Item,
    Heal,
    Shop
}
