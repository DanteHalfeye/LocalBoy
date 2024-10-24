using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuDontDestroyOnLoadKiller : MonoBehaviour
{
    private void Awake()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the active scene is the main menu
        if (scene.buildIndex == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
