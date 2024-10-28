using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternsToSpawn : MonoBehaviour
{
    public List<GameObject> enemyPatterns = new List<GameObject>();

    [SerializeField] private int currentEncounterListIndex = 0;
    [SerializeField] private int[] patternIDs;

    [SerializeField] private int delayBetweenSpawns;

    private int x = 2;
    private float y;

    private void Start()
    {
        SelectAmountOfPatterns();
        delayBetweenSpawns = (int)DelayBetweenSpawns();
        patternIDs = GetPatterns();
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

    public int SelectAmountOfPatterns()
    {
        int a = GameManager.Instance.CurrentLevelNumber;
        x = Mathf.RoundToInt(1 + (a / 2) + (Mathf.Pow(a, 3 / 2)  * 0.2f));
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


}
