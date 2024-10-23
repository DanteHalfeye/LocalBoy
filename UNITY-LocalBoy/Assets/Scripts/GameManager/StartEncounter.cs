using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEncounter : MonoBehaviour
{
    [SerializeField] private EnemeySpawner spawner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PatternsToSpawn patternsToSpawn = spawner.gameObject.GetComponent<PatternsToSpawn>();
            
            PlayLevelEncounter( patternsToSpawn.SelectAmountOfPatterns(), patternsToSpawn.GetPatterns(), patternsToSpawn.DelayBetweenSpawns());
        }
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        ItemEvents.TriggerOnRoomEntered();
    }
    private void PlayLevelEncounter(int encounterLevelListIndex)
    {
        spawner.InstantiateEnemies(encounterLevelListIndex);

    }

    private void PlayLevelEncounter(int amountOfPatterns, int[] patternsIDs, float delayBetweenSpawns)
    {
        spawner.InstantiateEnemies(amountOfPatterns, patternsIDs, delayBetweenSpawns);

    }
}
