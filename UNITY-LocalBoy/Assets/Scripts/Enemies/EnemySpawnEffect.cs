using System.Collections;
using UnityEngine;

public class EnemySpawnEffect : MonoBehaviour
{
    private Vector3 targetScale;
    private float growDuration = 1f; // How long the grow effect lasts
    private bool isGrowingComplete = false;
    EnemyShoot enemyShoot;
    private void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = Vector3.zero; // Start at 0 scale
        StartCoroutine(ScaleUp());
        enemyShoot = GetComponent<EnemyShoot>();
    }

    private IEnumerator ScaleUp()
    {
        float elapsedTime = 0f;

        while (elapsedTime < growDuration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / growDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure final scale is set correctly
        transform.localScale = targetScale;
        isGrowingComplete = true; // Mark the growing effect as complete

        // Now you can trigger the logic after the growing effect completes
        OnGrowEffectComplete();
    }

    private void OnGrowEffectComplete()
    {
        if (isGrowingComplete)
        {
            enemyShoot.enabled = true;
        }
    }
}
