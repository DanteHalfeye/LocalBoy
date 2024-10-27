using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHorizon : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();
    private List<float> values = new List<float>();


    private void Awake()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            values.Add(enemies[i].GetComponent<Shoot>().fuerza);
            enemies[i].GetComponent<Shoot>().fuerza *= 0.975f;
        }
    }

    private void OnDisable()
    {
        for(int i = 0;i < enemies.Count;i++)
        {
            enemies[i].GetComponent<Shoot>().fuerza = values[i];
        }
    }
}
