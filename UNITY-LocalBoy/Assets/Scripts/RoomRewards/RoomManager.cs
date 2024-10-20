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

    private List<DoorReward> doors;
    private BoxCollider2D spawnArea;

    [SerializeField]
    private Rewards currentReward;

    public static RoomManager instance { get; private set; }

    private EventInstance fondoInstance;


    private void Awake()
    {
        fondoInstance = AudioManager.CreateInstance("fondo-music");
        spawnArea = GetComponent<BoxCollider2D>();
        doors = new List<DoorReward>();

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        AudioManager.PlaySingleEmiter(fondoInstance);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            SpawnDoors();
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }

        AudioManager.StopEmiter(fondoInstance);
    }

    private void OnEnable()
    {
        StaticEventHandler.OnRoomCleared += SpawnDoors;
        StaticEventHandler.OnRoomCleared += SpawnRewards;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomCleared -= SpawnDoors;
        StaticEventHandler.OnRoomCleared -= SpawnRewards;
    }


    private void SpawnDoors()    
    {
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


    public void SpawnRewards()
    {
        GameObject prefab = Resources.Load <GameObject>("rewards/Prefabs/" + currentReward.ToString());
        if (prefab == null) return;

        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);

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
