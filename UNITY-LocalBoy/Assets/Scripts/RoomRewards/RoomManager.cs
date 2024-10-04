using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject doorPrefab;
    [SerializeField]
    private int minDoors;
    [SerializeField]
    private int maxDoors;

    private List<DoorReward> doors;

    [SerializeField]
    private Rewards currentReward;

    public static RoomManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destruir la instancia duplicada.
            return;
        }

        instance = this; // Asignar esta instancia como la única válida.
        DontDestroyOnLoad(gameObject); // Hacer que el objeto no se destruya al cargar una nueva escena.
    }

    private void OnDestroy()
    {
        // Asegúrate de limpiar la referencia estática al destruir la instancia.
        if (instance == this)
        {
            instance = null;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SpawnDoors;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SpawnDoors;
    }

    private void SpawnDoors(Scene scene, LoadSceneMode modo)
    {
        if (doors != null)
        {
            doors.Clear();
        }


        int doorQty = UnityEngine.Random.Range(minDoors, maxDoors + 1);

        for (int i = 0; i < doorQty; i++)
        {
            GameObject instance = Instantiate(doorPrefab);
            doors.Add(instance.GetComponent<DoorReward>());
        }

        //AsignDoors();
    }


    private void AsignDoors()
    {
        if (doors != null)
        {
            foreach (DoorReward door in doors)
            {
                door.AsignReward(SeleccionarEnumAleatorio<Rewards>());
            }
        }

    }


    private static T SeleccionarEnumAleatorio<T>() where T : Enum
    {
        T[] valores = (T[])Enum.GetValues(typeof(T));
        int indiceAleatorio = UnityEngine.Random.Range(0, valores.Length);
        return valores[indiceAleatorio];
    }











    public Rewards CurrentReward
    {
        get { return currentReward; }
        set { currentReward = value; }
    }
}
