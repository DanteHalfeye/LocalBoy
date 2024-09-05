using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]

public class EnemySpawner : SingletonMonobehaviour<EnemySpawner>
{
    private int enemiesToSpawn;
    private int currentEnemyCount;
    private int enemiesSpawnedSoFar;
    private int enemyMaxConcurretSpawnNumber;
    private Room currentRoom;
    private RoomEnemySpawnParameters roomEnemySpawnParameters;

    private void OnEnable()
    {
        //subscribe to room changed event
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;

    }

    private void OnDisable()
    {
        //unsubscribe from room changed event
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;
    }

    //process a change in room
    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        enemiesSpawnedSoFar = 0;
        currentEnemyCount = 0;

        currentRoom = roomChangedEventArgs.room;

        //if the room is a corridor or the entrance then return
        if (currentRoom.roomNodeType.isCorridorEW || currentRoom.roomNodeType.isCorridorNS || currentRoom.roomNodeType.isEntrance)
            return;

        //if the room has already been defeated, then return
        if (currentRoom.isClearedOfEnemies) return;

        //get random number of enemies to spawn
        enemiesToSpawn = currentRoom.GetNumberOffEnemiesToSpawn(GameManager.Instance.GetCurrentDungeonLevel());

        //Get room enemy spawn parameters
        roomEnemySpawnParameters = currentRoom.GetRoomEnemySpawnParameters(GameManager.Instance.GetCurrentDungeonLevel());

        //if no enemies to spawn return
        if (enemiesToSpawn == 0)
        {
            //Mark thhe room as creared
            currentRoom.isClearedOfEnemies = true;

            return;
        }

        //get current number of enemies to spawn
        enemyMaxConcurretSpawnNumber = GetConcurrentEnemies();

        ////Lock doors
        //currentRoom.instantiatedRoom.LockDoors;

        //spawn enemies
        SpawnEnemies();


      
        


    }

    //Spawn the enemies
    private void SpawnEnemies()
    {
        //Set gameState engaginf enemies
        if(GameManager.Instance.gameState == GameState.playingLevel)
        {
            GameManager.Instance.previousGameState = GameState.playingLevel;
            GameManager.Instance.gameState = GameState.engagingEnemies;
        }

        StartCoroutine(SpawnEnemiesRoutine());
    }

    //Spawn enemies coroutine
    private IEnumerator SpawnEnemiesRoutine()
    {
        Grid grid = currentRoom.instantiatedRoom.grid;

        //create an instance of the helper class used to select a random enemy
        RandomSpawnableObject<EnemyDetailsSO> randomEnemyHelperClass = new RandomSpawnableObject<EnemyDetailsSO>(currentRoom.enemiesByLevelList);

        //Check we have somewhere to spawn enemies
        if(currentRoom.spawnPositionArray.Length > 0)
        {
            //Loop through to create all the enemies
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                //wait until current enemy couunt is less than the max concurret enemies
                while (currentEnemyCount >= enemyMaxConcurretSpawnNumber)
                {
                    yield return null;
                }

                Vector3Int cellPosition = (Vector3Int)currentRoom.spawnPositionArray[Random.Range(0, currentRoom.spawnPositionArray.Length)];

                //Create enemy - get next enemy type to spawn
                CreateEnemy(randomEnemyHelperClass.GetItem(), grid.CellToWorld(cellPosition));

                yield return new WaitForSeconds(GetEnemySpawnInterval());

            }
        }
    }

    //get randdom spawn interval between the minimum and maximum values
    private float GetEnemySpawnInterval()
    {
        return (Random.Range(roomEnemySpawnParameters.minSpawnInterval, roomEnemySpawnParameters.maxSpawnInterval));
    }

    //get random number of concurrent enemies between the minimum and maximum values
    private int GetConcurrentEnemies()
    {
        return ( Random.Range(roomEnemySpawnParameters.minConcurrentEnemies, roomEnemySpawnParameters.maxConcurrentEnemies));
    }

    //create an enemy in the specified position
    private void CreateEnemy(EnemyDetailsSO enemyDetails, Vector3 position)
    {
        //keep track of the number of enemies spawned so far
        enemiesSpawnedSoFar++;

        //add one to the current enemy count - this is reduced when an enemy is destroyed
        currentEnemyCount++;

        //Get current dungeon level
        DungeonLevelSO dungeonLevel = GameManager.Instance.GetCurrentDungeonLevel();

        //instantiate enemy
        GameObject enemy = Instantiate(enemyDetails.enemyPrefab, position, Quaternion.identity, transform);

        //initialize enemy
        enemy.GetComponent<Enemy>().EnemyInitialization(enemyDetails, enemiesSpawnedSoFar, dungeonLevel);
    }

}
