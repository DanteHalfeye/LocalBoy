using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Linq;
using static UnityEngine.ParticleSystem;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Hay mas de una instancia del audio manager");
        }
        instance = this;
    }

    /// <summary>
    /// Esta función reproduce un audio simple.
    /// </summary>
    public void PlayOneShot(string track, Vector3 Origin)
    {
        RuntimeManager.PlayOneShot(LinqGetReference(track), Origin);
    }

    /// <summary>
    /// Esta función crea una instancia de audio para la correcta manipulacion del evento.
    /// </summary>
    /// <param name="track">Nombre del audio en FMODEvents.</param>
    /// <returns>EventInstance.</returns>
    public EventInstance CreateInstance(string track)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(LinqGetReference(track));
        return eventInstance;
    }

    /// <summary>
    /// Esta función permite manipular los parametros de un evento, este tiene que estar instanciado y en reproduccion.
    /// </summary>
    /// <param name="track">Nombre del audio en FMODEvents.</param>
    /// <param name="paremeterName">Nombre del parametro a manipular.</param>
    /// <param name="value">Valor que se le da al parametro.</param>
    public void SetParameter(EventInstance track, string paremeterName, float value)
    {
        track.setParameterByName(paremeterName, value);
    }

    /// <summary>
    /// Esta función reproducira un emisor y solo permitira la reproduccion de 1.
    /// </summary>
    /// <param name="instance">Instancia que reproducira el emisor.</param>
    public void PlaySingleEmiter(EventInstance instance) 
    {
        PLAYBACK_STATE playbackState;
        instance.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            instance.start();
        }
    }

    /// <summary>
    /// Esta función detendra la reproduccion de la instancia si se esta reproduciendo.
    /// </summary>
    /// <param name="instance">Instancia que detener.</param>
    public void StopEmiter(EventInstance instance)
    {
        PLAYBACK_STATE playbackState;
        instance.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.PLAYING))
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    /// <summary>
    /// Esta función reproducira un emisor y permitira la reproduccion de varios.
    /// </summary>
    /// <param name="instance">Instancia que reproducira el emisor.</param>
    public void PlayMultipleEmiter(EventInstance track)
    {
        PLAYBACK_STATE playbackState;
        track.getPlaybackState(out playbackState);
        track.start();
    }

    /// <summary>
    /// Esta función asigna la posicion 3D de un emisor, este se declara si el evento usa un spatializer.
    /// </summary>
    /// <param name="instance">Instancia para asignar los atributos.</param>
    /// <param name="position">Posicion 3D a asignar.</param>
    public void Set3DAtributes(EventInstance instance, Vector3 position)
    {
        instance.set3DAttributes(position.To3DAttributes());
    }

    /// <summary>
    /// Esta función se usa para obtener el EventReference de la lista FMODEvents solo con el nombre.
    /// </summary>
    /// <param name="trackName">Nombre del audio en FMODEvents.</param>
    /// <returns>EventReference.</returns>
    public EventReference LinqGetReference(string trackName)
    {
        EventReference track = FMODEvents.instance.events.FirstOrDefault(e => e.name == trackName).evento;
        return track;
    }

}
