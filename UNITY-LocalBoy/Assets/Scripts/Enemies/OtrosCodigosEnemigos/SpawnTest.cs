using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public RoomTemplateSO roomTemplateSO;
    private List<SpawnableObjectsByLevel<EnemyDetailsSO>> testLevelSpawnList;
    private RandomSpawnableObject<EnemyDetailsSO> randomEnemyHelperClass;
    private GameObject instantiatedEnemy;

    private void Start()
    {
        //get test level spawn list form dungeon room template
        testLevelSpawnList = roomTemplateSO.enemiesByLevelList;

        //create RandomSpawnabeObject helper class
        randomEnemyHelperClass = new RandomSpawnableObject<EnemyDetailsSO>(testLevelSpawnList);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("tecla presionada");
            if(instantiatedEnemy != null)
            {
                Destroy(instantiatedEnemy);
            }

            EnemyDetailsSO enemyDetails = randomEnemyHelperClass.GetItem();

            if(enemyDetails != null)
            {
                instantiatedEnemy = Instantiate(enemyDetails.enemyPrefab, HelperUtilities.GetSpawnPositionNearestToPlayer(HelperUtilities.GetMouseWorldPosition()), Quaternion.identity);
            }
        }
    }
}
