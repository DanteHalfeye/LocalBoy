using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternsToSpawn : MonoBehaviour
{
    public List<GameObject> enemyPatterns = new List<GameObject>();

    [SerializeField] private int currentEncounterListIndex = 0;
    [SerializeField] private int[] patternIDs;

    [SerializeField] private int delayBetweenSpawns;

    private float x = 2;
    private float y;

    private void Start()
    {
        SelectAmountOfPatterns();
        delayBetweenSpawns = (int)DelayBetweenSpawns();
        patternIDs = GetPatterns();

        // Call method to spawn patterns with the selected amount and delay
        StartCoroutine(SpawnPatterns());
    }

    public int[] GetPatterns()
    {
        // Create an array to hold the pattern IDs
        int[] selectedPatternIDs = new int[x];

        // Randomly select patterns from the list
        for (int i = 0; i < x; i++)
        {
            selectedPatternIDs[i] = Random.Range(0, enemyPatterns.Count);
        }

        return selectedPatternIDs;
    }

    public float SelectAmountOfPatterns()
    {
        x = Mathf.Pow(2, GameManager.Instance.CurrentLevelNumber - 1);
        return x;
    }

    public float DelayBetweenSpawns()
    {
        y = 5 - 4f / 49 * (GameManager.Instance.CurrentLevelNumber - 1);
        if (y <= 1)
        {
            y = 1;
        }
        return y;
    }

    private IEnumerator SpawnPatterns()
    {
        // Loop through the selected pattern IDs to spawn them
        foreach (int patternID in patternIDs)
        {
            // Spawn the enemy pattern (assuming you have a method to handle this)
            SpawnEnemyPattern(enemyPatterns[patternID]);

            // Wait for the delay before spawning the next pattern
            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }

    private void SpawnEnemyPattern(GameObject pattern)
    {
        // Implement the logic to spawn the enemy pattern in your scene
        Instantiate(pattern, transform.position, Quaternion.identity);
    }
}
