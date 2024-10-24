using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHint : MonoBehaviour
{
    private float floatHeight = 0.5f;
    private float floatDuration = 2.0f;
    private Vector2 originalPosition;


    private Sprite icon;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetIcon(transform.parent.GetComponent<DoorReward>().RewardType);
    }

    private void Float()
    {
        LeanTween.moveY(gameObject, originalPosition.y + floatHeight, floatDuration)
            .setEaseInOutSine()
            .setLoopPingPong();
    }

    private void GetIcon(Rewards batch)
    {
        /*
        icon = Resources.Load<Sprite>("Rewards/Icons/" + batch.ToString());
        if (icon != null)
        {
            spriteRenderer.sprite = icon;
        }
        else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("Rewards/Icons/Missing");
        }
        */


        if (batch == Rewards.Heal)
        {
            spriteRenderer.color = Color.red;
        }
        else if (batch == Rewards.Money)
        {
            spriteRenderer.color = Color.green;
        }
        else if (batch == Rewards.Item)
        {
            spriteRenderer.color = Color.blue;
        }
        else if (batch == Rewards.Shop)
        {
            spriteRenderer.color = Color.yellow;
        }


        Float();
    }
}
