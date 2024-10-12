using FMODUnity;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{

    [SerializeField] private int _currentLevelNumber;

    private PlayerMovement _player;

    float _amountOfEnemies, _amountOfDeadEnemies, _ammountOfPatterns, _currentAmountOfPatterns;


    [HideInInspector] public GameState gameState;
    [HideInInspector] public GameState previousGameState;


    public int CurrentLevelNumber { get { return _currentLevelNumber; } }
    public int AmmountOfPatterns { set { _ammountOfPatterns = value; } }
    public int CurrentAmmountOfPatterns { set { _currentAmountOfPatterns = value; } }
    private void Start()
    {
        _amountOfEnemies = 0;
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
        StaticEventHandler.OnEntitySpawned += HandleEntitySpawned;
        StaticEventHandler.OnEntityDied += HandleEntityDied;
        SceneManager.sceneLoaded += IncrementLevelNumber;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnEntitySpawned -= HandleEntitySpawned;
        StaticEventHandler.OnEntityDied -= HandleEntityDied;
        SceneManager.sceneLoaded -= IncrementLevelNumber;
    }
    public void IncrementLevelNumber(Scene scene, LoadSceneMode modo)
    {
        _currentLevelNumber++;
    }

    public PlayerMovement GetPlayer()
    {
        return _player;
    }

    public void SetPlayer(PlayerMovement currentPlayer)
    {
        _player = currentPlayer;
    }
    
    private void Update()
    {
        // Testing
        if (Input.GetKeyDown(KeyCode.R))
            HandleGameState();

    }
    private void HandleEntitySpawned()
    {
        _amountOfEnemies++;
    }

    private void HandleEntityDied()
    {
        _amountOfDeadEnemies++;
        if (_currentAmountOfPatterns <_ammountOfPatterns)
        {
            return;
        }
        if (_amountOfDeadEnemies >= _amountOfEnemies)
        {
            RoomCleared();
        }
    }

    private void RoomCleared()
    {
        StaticEventHandler.NotifyRoomCleared();
        print("Room Cleared Good Job you are great and all that stuff");
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
    
    
    

}
