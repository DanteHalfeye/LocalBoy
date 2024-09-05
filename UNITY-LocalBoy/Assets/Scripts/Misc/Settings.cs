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

    ////ESTO LO PONGO POR SI ALGO, ANIMACION 
    //#region ANIMATOR PARAMETERS
    //// Animator parameters - Player
    //public static int aimUp = Animator.StringToHash("aimUp");
    //public static int aimDown = Animator.StringToHash("aimDown");
    //public static int aimUpRight = Animator.StringToHash("aimUpRight");
    //public static int aimUpLeft = Animator.StringToHash("aimUpLeft");
    //public static int aimRight = Animator.StringToHash("aimRight");
    //public static int aimLeft = Animator.StringToHash("aimLeft");
    //public static int isIdle = Animator.StringToHash("isIdle");
    //public static int isMoving = Animator.StringToHash("isMoving");
    //public static int rollUp = Animator.StringToHash("rollUp");
    //public static int rollRight = Animator.StringToHash("rollRight");
    //public static int rollLeft = Animator.StringToHash("rollLeft");
    //public static int rollDown = Animator.StringToHash("rollDown");
    //public static float baseSpeedForPlayerAnimations = 8f;

    //// Animator parameters - Enemy
    //public static float baseSpeedForEnemyAnimations = 3f;

    ////Animator parameters - Enemy
    //public static int open = Animator.StringToHash("open");

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
