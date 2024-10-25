using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    EventInstance musicInstance;
    private void Awake()
    {
        musicInstance = AudioManager.CreateInstance("fondo-music_2");
        AudioManager.PlaySingleEmiter(musicInstance);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
