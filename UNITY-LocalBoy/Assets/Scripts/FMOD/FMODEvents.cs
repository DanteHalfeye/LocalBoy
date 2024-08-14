using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public List<EventsList> events = new List<EventsList>();

    public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Mas de un fmod events script en la escena");
        }
        instance = this;
    }
}

[System.Serializable]
public class EventsList
{
    [field: SerializeField] public string name;
    [field: SerializeField] public EventReference evento { get; private set; }
}
