using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{

    #region Tooltip
    [Tooltip("Populate with the starting dungeon level for testing, first level = 0")]
    #endregion Tooltip
    [SerializeField] private int currentEncounterListIndex = 0;
    [SerializeField] private int[] amountOfPatternsInRoom;
    [SerializeField] private int delayBetweenSpawns;
    [SerializeField] private bool multiplePatterns;

    private PlayerMovement player;

    [SerializeField]private EnemeySpawner spawner;


    [HideInInspector] public GameState gameState;
    [HideInInspector] public GameState previousGameState;



    private void Start()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = false;
        QualitySettings.vSyncCount = 0;

        previousGameState = GameState.gameStarted;
        gameState = GameState.gameStarted;


        HandleGameState();
    }

    public PlayerMovement GetPlayer()
    {
        return player;
    }

    public void SetPlayer(PlayerMovement currentPlayer)
    {
        player = currentPlayer;
    }
    /*
    /// <summary>
    /// Get the current room the player is in
    /// </summary>
    public Room GetCurrentRoom()
    {
        ItemEvents.TriggerOnRoomEntered();
        return currentRoom;
    }
    */
    private void Update()
    {
        // Testing
        if (Input.GetKeyDown(KeyCode.R))
            HandleGameState();
    }


    public void StartGame()
    {
        HandleGameState();
    }



    /// <summary>
    /// Handle game state
    /// </summary>
    private void HandleGameState()
    {
        // Handle game state
        switch (gameState)
        {
            case GameState.gameStarted:
                // Play first level
                if (!multiplePatterns)
                {
                    PlayLevelEncounter(currentEncounterListIndex);

                }
                else
                {
                    PlayLevelEncounter(amountOfPatternsInRoom.Length, amountOfPatternsInRoom, delayBetweenSpawns);
                }

                //gameState = GameState.playingLevel; 
                break;

        }
    }
    /*
    /// <summary>
    /// Set the current room the player in in
    /// </summary>
    public void SetCurrentRoom(Room room)
    {
        previousRoom = currentRoom;
        currentRoom = room;

        //// Debug
        //Debug.Log(room.prefab.name.ToString());
    }
    */
    
    private void PlayLevelEncounter(int encounterLevelListIndex)
    {
        spawner.InstantiateEnemies(encounterLevelListIndex);

    }

    private void PlayLevelEncounter(int amountOfPatterns, int[] patternsIDs, float delayBetweenSpawns)
    {
        spawner.InstantiateEnemies(amountOfPatterns, patternsIDs, delayBetweenSpawns);

    }

}
