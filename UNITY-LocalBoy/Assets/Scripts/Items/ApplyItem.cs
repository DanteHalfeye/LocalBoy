using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyItem : MonoBehaviour
{
    [SerializeField]
    private ItemSO item;

    private SpriteRenderer spriteRender;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRender.sprite = item.Icon;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerActor actor = other.GetComponent<PlayerActor>();

            actor.PickUpItem(item);

            Destroy(gameObject);
        }
    }
}
