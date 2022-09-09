using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public GameObject MovementAudioSourcesContainer;
    public GameObject PUzle1AudioSourcesContainer;
    private List<AudioSource> MovementAudioSources;
    private List<AudioSource> Puzzle1AudioSources;

    private void Start()
    {
        MovementAudioSources = new List<AudioSource>();
        Puzzle1AudioSources = new List<AudioSource>();
        var sources = MovementAudioSourcesContainer.GetComponentsInChildren<AudioSource>().ToList();
        for (int i = 0; i < sources.Count; i++)
        {
            MovementAudioSources.Add(sources[i]);
        }
        sources = PUzle1AudioSourcesContainer.GetComponentsInChildren<AudioSource>().ToList();
        for (int i = 0; i < sources.Count; i++)
        {
            Puzzle1AudioSources.Add(sources[i]);
        }
    }


    public void PlayMovementSound(int i)
    {
        MovementAudioSources[i].Play();
    }

    public void PlayPuzzle1Sound(int i)
    {
        Puzzle1AudioSources[i].Play();
    }
}
