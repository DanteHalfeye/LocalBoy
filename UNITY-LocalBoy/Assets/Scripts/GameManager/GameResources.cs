using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get 
        { if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            } 
        return instance;
        }
    }

    #region Header DUNGEON
    [Space(10)]
    [Header("DUNGEON")]
    #endregion
    #region Tooltip
    [Tooltip("Populate with the dungeon RoomNodeTypeListSO")]
    #endregion

    public RoomNodeTypeListSO roomNodeTypeList;

    #region Header MATERIALS
    [Space(10)]
    [Header("MATERIALS")]
    #endregion
    #region Tooltip
    [Tooltip("Dimmed Material")]
    #endregion
    public Material dimmedMaterial;

//    #region Header SPECIAL TILEMAP TILES
//    [Space(10)]
//    [Header("SPECIAL TILEMAP TILES")]
//    #endregion Header SPECIAL TILEMAP TILES
//    #region Tooltip
//    [Tooltip("Collision tiles that enemies can navigate to")]
//    #endregion Tooltip
//    public TileBase[] enemyUnwalkableCollisionTilesArray;
//    #region Tooltip
//    [Tooltip("Preferred path tile for enemy navigation")]
//    #endregion Tooltip
//    public TileBase preferredEnemyPathArray;

//    #region Validation
//#if UNITY_EDITOR
//    //validando los detalles de scriptable objects detectados
//    private void OnValidate()
//    {
        
//        HelperUtilities.ValidateCheckEnumerableValues(this,nameof(enemyUnwalkableCollisionTilesArray), enemyUnwalkableCollisionTilesArray);
//        HelperUtilities.ValidateCheckNullValue(this, nameof(preferredEnemyPathTile), preferredEnemyPathTile);
//    }
//#endif
//    #endregion
}
