using UnityEngine;

[System.Serializable]

public class RoomEnemySpawnParameters
{
    #region ToolTip
    [Tooltip("Defines the dungeon level for this room with regard to how many enemies in total should be spawned")]
    #endregion ToolTip
    public DungeonLevelSO dungeonLevel;

    #region ToolTip
    [Tooltip("The minimum number of enemies to spawn in this room for this dungeon level. the actual number will be a random value between the min and max values")]
    #endregion ToolTip
    public int minTotalEnemiesToSpawn;

    #region ToolTip
    [Tooltip("The maximum number of enemies to spawn in this room for this dungeon level. The actual number will be a random btw min and max value")]
    #endregion ToolTip
    public int maxTotalEnemiesToSpawn;

    #region ToolTip
    [Tooltip("The minimum number of concurrent enemies to spawn in this room for this dungeon level. the actual number will eill be a random vaalue btw the min and max vualues")]
    #endregion ToolTip
    public int minConcurrentEnemies;

    #region ToolTip
    [Tooltip("The maximum number of concurrent enemies to spawn in this room for this dungeon level. the actual number will eill be a random vaalue btw the min and max vualues")]
    #endregion ToolTip
    public int maxConcurrentEnemies;

    #region ToolTip
    [Tooltip("The minimum spawn intervals in seconds for enemies in this room for this dungeon level. The actual number will be a random value")]
    #endregion ToolTip
    public int minSpawnInterval;

    #region ToolTip
    [Tooltip("The maximum spawn intervals in seconds for enemies in this room for this dungeon level. The actual number will be a random value")]
    #endregion ToolTip
    public int maxSpawnInterval;

}
