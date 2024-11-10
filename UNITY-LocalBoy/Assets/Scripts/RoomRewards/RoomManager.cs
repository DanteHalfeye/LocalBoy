using FMOD.Studio;
using FMODUnity;
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
    [SerializeField]
    private float doorTimer;

    private List<DoorReward> doors;
    private BoxCollider2D spawnArea;

    [SerializeField]
    private Rewards currentReward;

    public static RoomManager instance { get; private set; }

    private EventInstance fondoInstance;

    public EventInstance FondoInstance => fondoInstance;

    private void Awake()
    {
        ItemPool.LoadPool();
        spawnArea = GetComponent<BoxCollider2D>();
        doors = new List<DoorReward>();

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(MusicCountDown());

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator MusicCountDown()
    {
        float count = 0.5f;

        while (count > 0)
        {
            count -= Time.deltaTime;
            yield return null;
        }
        fondoInstance = AudioManager.CreateInstance("fondo-music_2");
        AudioManager.PlaySingleEmiter(fondoInstance);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            SpawnRewards();
        }
    }

    private void OnEnable()
    {
        StaticEventHandler.OnRoomCleared += SpawnDoorsCall;
        StaticEventHandler.OnRoomCleared += SpawnRewards;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomCleared -= SpawnDoorsCall;
        StaticEventHandler.OnRoomCleared -= SpawnRewards;
    }


    private void SpawnDoorsCall()
    {
        StartCoroutine(TimerDoors());
    }

    private void SpawnDoors()    
    {
        AudioManager.PlayOneShot("level-clear", new Vector3(0, 0, 0));
        Vector2 areaSize = spawnArea.size;
        Vector2 areaCenter = spawnArea.transform.position + (Vector3)spawnArea.offset;



        if (doors != null)
        {
            doors.Clear();
        }


        int doorQty = UnityEngine.Random.Range(minDoors, maxDoors + 1);
        float doorSpacing = areaSize.x / (doorQty + 1);
        for (int i = 0; i < doorQty; i++)
        {
            float xPos = areaCenter.x - (areaSize.x / 2) + doorSpacing * (i + 1);
            Vector2 spawnPosition = new Vector2(xPos, areaCenter.y);
            GameObject instance = Instantiate(doorPrefab, spawnPosition, Quaternion.identity);
            doors.Add(instance.GetComponent<DoorReward>());
        }

        AsignDoors();
    }

    private void AsignDoors()
    {
        if (doors != null)
        {
            foreach (DoorReward door in doors)
            {
                door.AsignReward(SeleccionarEnumAleatorio<Rewards>());
                Debug.Log(door.RewardType.ToString());
            }
        }
    }

    private IEnumerator TimerDoors()
    {
        float timer = doorTimer;
        while (timer >= 0) 
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        SpawnDoors();
    }

    public void SpawnRewards()
    {
        GameObject prefab = Resources.Load <GameObject>("rewards/Prefabs/" + currentReward.ToString());
        if (prefab == null) return;

        GameObject instance = Instantiate(prefab, new Vector3(0, -20, 0 ), Quaternion.identity);

    }

    private static T SeleccionarEnumAleatorio<T>() where T : Enum
    {
        // Definir los pesos en el mismo orden que los valores del enum.
        int[] weights = new int[] { 40, 20, 10, 30 };

        // Obtener los valores del enum.
        T[] valores = (T[])Enum.GetValues(typeof(T));

        // Verificar que los pesos coincidan con el número de valores.
        if (weights.Length != valores.Length)
        {
            throw new ArgumentException("El número de pesos no coincide con el número de valores en el enum.");
        }

        // Calcular la suma total de los pesos.
        int totalWeight = 0;
        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        // Generar un número aleatorio entre 0 y el total de los pesos.
        int randomValue = UnityEngine.Random.Range(0, totalWeight);

        // Determinar qué valor corresponde al número aleatorio basado en los pesos.
        int cumulativeWeight = 0;
        for (int i = 0; i < valores.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return valores[i];
            }
        }

        throw new InvalidOperationException("No se pudo seleccionar un valor aleatorio del enum.");
    }

    public Rewards CurrentReward
    {
        get { return currentReward; }
        set { currentReward = value; }
    }

    public EventInstance MusicFondo
    {
        get { return fondoInstance; }
    }
}
