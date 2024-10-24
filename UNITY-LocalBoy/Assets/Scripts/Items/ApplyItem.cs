using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyItem : MonoBehaviour
{
    private SpriteRenderer spriteRender;

    private ItemSO item;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        item = ItemPool.GetItemFromPool();
        spriteRender.sprite = item.Icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerActor actor = other.GetComponent<PlayerActor>();
            UIText.instance.Show(item);
            AudioManager.PlayOneShot("pick-item", gameObject.transform.position);
            Debug.Log(item.Name);
            item.Initialize();
            if (!item.CanStack)
            {
                ItemPool.RemoveItemFromPool(item);
            }

            actor.PickUpItem(item);
            Destroy(this.gameObject);



        }
    }
}
