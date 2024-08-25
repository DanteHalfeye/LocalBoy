using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{
    #region header DUNGEON LEVELS
    [Space(10)]
    [Header("DUNGEON LEVELS")]
    #endregion DUNGEON LEVELS

    #region Tooltip
    [Tooltip("Populate with dungeon level scriptable objects")]
    #endregion Tooltip

    [SerializeField] private List<DungeonLevelSO> dungeonLevelList;

    #region Tooltip
    [Tooltip("Populate with the starting dungeon level for testing, first level = 0")]
    #endregion Tooltip
    [SerializeField] private int currentDungeonLevelListIndex = 0;

    [HideInInspector] public GameState gameState;

    
    private Room currentRoom;
    private Room previousRoom;

    //private PlayerDetailsSO playerDetails;
   // private Player player;

   

    private void Start()
    {
        gameState = GameState.gameStarted;
    }

    private void Update()
    {
        // Testing
        if (Input.GetKeyDown(KeyCode.R))
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
                PlayDungeonLevel(currentDungeonLevelListIndex);

                //gameState = GameState.playingLevel; 
                break;

        }
    }

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        // Build dungeon for level
       bool dungeonBuiltSuccessfully = DungeonBuilder.Instance.GenerateDungeon(dungeonLevelList[dungeonLevelListIndex]);
        if (!dungeonBuiltSuccessfully)
        {
            Debug.LogError("Couldn't build dungeon from specified rooms and node graphs");
        }

        // Call static event that room has changed.
        StaticEventHandler.CallRoomChangedEvent(currentRoom);
    }

    /// <summary>
    /// Handle room changed event
    /// </summary>
    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        SetCurrentRoom(roomChangedEventArgs.room);
    }

    public void SetCurrentRoom(Room room)
    {
        previousRoom = currentRoom;
        currentRoom = room;

        //// Debug
        //Debug.Log(room.prefab.name.ToString());
    }

    /// <summary>
    /// Get the player
    /// </summary>
    //public Player GetPlayer()
    //{
    //    return player;
    //}


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(dungeonLevelList), dungeonLevelList);
    }
#endif
    #endregion Validation



}
