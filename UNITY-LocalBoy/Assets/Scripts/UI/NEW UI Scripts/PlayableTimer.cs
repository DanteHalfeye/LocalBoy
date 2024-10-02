using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayableTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float _elapsedTime;
    private NewHealthController _healthController;

    private void Awake()
    {
        _healthController = FindObjectOfType<NewHealthController>();
    }

    private void OnEnable()
    {
        _elapsedTime = 0f;
    }

    void Update()
    {
        if (_healthController != null && !_healthController.IsPlayerDead)
        {
            _elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(_elapsedTime / 60);
            int seconds = Mathf.FloorToInt(_elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((_elapsedTime * 100) % 100);
            timerText.text = string.Format("{0:00}:{1:00}'{2:00}''", minutes, seconds, milliseconds);
        }
    }
}
