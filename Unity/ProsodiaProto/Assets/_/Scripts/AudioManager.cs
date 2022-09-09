using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public GameObject MovementAudioSourcesContainer;
    private List<AudioSource> MovementAudioSources; 

    private void Start()
    {
        MovementAudioSources = new List<AudioSource>();
        var Sources = MovementAudioSourcesContainer.GetComponentsInChildren<AudioSource>().ToList();
        MovementAudioSources.AddRange(Sources);
    }


    public void PlaySound()
    {
        Debug.Log("playing sound");
        MovementAudioSources[0].Play();
    }
}
