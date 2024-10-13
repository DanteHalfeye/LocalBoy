using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Money : MonoBehaviour
{
    [SerializeField]
    private int amount;
    private Counter contador;

    private void Awake()
    {
        contador = GameObject.FindGameObjectWithTag("Score").GetComponent<Counter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            contador.AddScore(amount, false);
        }
    }
}
