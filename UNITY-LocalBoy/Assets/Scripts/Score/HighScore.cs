using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighScore : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    public static HighScore instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    public void SaveHighScore(float score)
    {
        float currentHighScore = GetHighScore();

        if (score > currentHighScore)
        {
            PlayerPrefs.SetFloat(HighScoreKey, score);
            PlayerPrefs.Save();
            Debug.Log("Nuevo High Score: " + score);
        }
    }

    public float GetHighScore()
    {
        return PlayerPrefs.GetFloat(HighScoreKey, 0);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        PlayerPrefs.Save();
    }
}
