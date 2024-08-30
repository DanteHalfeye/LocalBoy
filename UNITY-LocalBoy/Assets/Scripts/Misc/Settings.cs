using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings 
{
    #region DUNGEON BUILD SETTINGS
    public const int maxDungeonRebuildAttemptsForRoomGraph = 100;
    public const int maxDungeonBuildAttempts = 10;
    #endregion
    #region ROOM SETTINGS

    public const int maxChildCorridors = 3; // Max number of child corridors leading to a room. -
                                            // maximun should be 3 althout this is not recomended since it can cause the dungeon building to fail since the rooms are more likely to not fit together

    #endregion


    #region GAMEOBJECT TAGS
    public const string playerTag = "Player";
    
    #endregion

    #region ASTAR PATHFINDING PARAMETERS
    public const int defaultAStarMovementPenalty = 40;
    public const int preferredPathAStarMovementPenalty = 1;
    public const int targetFrameRateToSpreadPathfindingOver = 60;
    public const float playerMoveDistanceToRebuildPath = 3f;
    public const float enemyPathRebuildCooldown = 2f;

    #endregion



    
}
