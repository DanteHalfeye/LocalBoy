using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{

    [SerializeField] private int currentLevelNumber;

    private PlayerMovement player;

    


    [HideInInspector] public GameState gameState;
    [HideInInspector] public GameState previousGameState;


    public int CurrentLevelNumber { get { return currentLevelNumber; } }

    private void Start()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = false;
        QualitySettings.vSyncCount = 0;

        previousGameState = GameState.gameStarted;
        gameState = GameState.gameStarted;

        //currentLevelNumber = 1;
        HandleGameState();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += IncrementLevelNumber;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= IncrementLevelNumber;
    }
    public void IncrementLevelNumber(Scene scene, LoadSceneMode modo)
    {
        currentLevelNumber++;
    }

    public PlayerMovement GetPlayer()
    {
        return player;
    }

    public void SetPlayer(PlayerMovement currentPlayer)
    {
        player = currentPlayer;
    }
    
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
                //game has started
                break;
            case GameState.playingLevel:
                // Play first level
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
    
    

}
