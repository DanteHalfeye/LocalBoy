using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawner : MonoBehaviour
{
    List<GameObject> enemyPatterns;

    int enemiesInPattern;
    int enemiesDefeatedInPattern;

    private void Start()
    {
        enemyPatterns = GetComponent<PatternsToSpawn>().enemyPatterns;
    }
    public void InstantiateEnemies(int id)
    {
        GameObject currentPatterns = Instantiate(enemyPatterns[id]);
        AmountOfEnemiesInCurrentPatern(id);
        currentPatterns.transform.position = Vector3.zero;
    }

    public void InstantiateEnemies(int amountOfPatterns, int[] patternIDs, float delayBetweenSpawns)
    {
        StartCoroutine(SpawnPatternsSequentially(amountOfPatterns, patternIDs, delayBetweenSpawns));
    }

    private void AmountOfEnemiesInCurrentPatern(int id)
    {
        foreach (Transform child in enemyPatterns[id].transform)
        {
            EnemyShoot enemy = child.GetComponent<EnemyShoot>();
            if (enemy != null)
            {
                enemiesInPattern++;
            }
        }
    }

    private IEnumerator SpawnPatternsSequentially(int amountOfPatterns, int[] patternIDs, float delayBetweenSpawns)
    {
        for (int i = 0; i < amountOfPatterns; i++)
        {
            int patternId = patternIDs[i];

            // Instantiate the current pattern
            GameObject currentPatterns = Instantiate(enemyPatterns[patternId]);
            currentPatterns.transform.position = Vector3.zero;
            SendPatternsRemaining(amountOfPatterns, i+1);
            // Optionally count enemies in the pattern
            AmountOfEnemiesInCurrentPatern(patternId);

            // Wait for the specified delay before spawning the next pattern
            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }

    private void SendPatternsRemaining(int amountOfPatterns, int currentAmount)
    {
        GameManager.Instance.AmmountOfPatterns = amountOfPatterns;
        GameManager.Instance.CurrentAmmountOfPatterns = currentAmount;
    }
}
