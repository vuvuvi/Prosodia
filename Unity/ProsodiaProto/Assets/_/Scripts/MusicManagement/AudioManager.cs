using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public GameObject MovementAudioSourcesContainer;
    public GameObject Puzzle1AudioSourcesContainer;
    private List<AudioSource> MovementAudioSources;
    private List<AudioSource> Puzzle1AudioSources;
    private Melody melodyToPlay;
    private float timeForNextNotePlayed;
    public float TimeBetweenNotAutoPlayed = 1;
    private List<AudioSource> notesCurrentlyPlaying;
    private Dictionary<string, List<AudioSource>> AudioSources;
    private bool isInit = false;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (isInit) return;
        GetAudioSources();
        notesCurrentlyPlaying = new List<AudioSource>();
        timeForNextNotePlayed = Time.time + TimeBetweenNotAutoPlayed;
        melodyToPlay = new Melody();
        MovementAudioSources = new List<AudioSource>();
        Puzzle1AudioSources = new List<AudioSource>();
        var sources = MovementAudioSourcesContainer.GetComponentsInChildren<AudioSource>().ToList();
        for (int i = 0; i < sources.Count; i++)
        {
            MovementAudioSources.Add(sources[i]);
        }
        sources = Puzzle1AudioSourcesContainer.GetComponentsInChildren<AudioSource>().ToList();
        for (int i = 0; i < sources.Count; i++)
        {
            Puzzle1AudioSources.Add(sources[i]);
        }
        isInit = true;
    }

    internal string GetSoundName(int i)
    {
        return AudioSources.Keys.ToList()[i + 1].ToString();
    }

    private void GetAudioSources()
    {
        AudioSources = new Dictionary<string, List<AudioSource>>();
        var sources = GetComponentsInChildren<AudioSource>().ToList();
        for (int i = 0; i < sources.Count; i++)
        {
            var source = sources[i];
            var parentName = source.transform.parent.name;
            if (!AudioSources.ContainsKey(parentName))
                AudioSources.Add(parentName, new List<AudioSource>() { });
            AudioSources[parentName].Add(source);
        }
    }

    private void Update()
    {
        if (Time.time > timeForNextNotePlayed && melodyToPlay.Notes.Count > 0)
        {
            var note = Puzzle1AudioSources[melodyToPlay.Notes[0]];
            note.Play();
            melodyToPlay.RemoveFromTheBeginning(1);
            timeForNextNotePlayed = Time.time + TimeBetweenNotAutoPlayed;
        }
    }

    internal void Play(Melody melody)
    {
        melodyToPlay.Add(melody);
    }

    public void PlayNote(int note, string sound, bool stopsTheOthers = false)
    {
        if (stopsTheOthers)
        {
            for (int i = notesCurrentlyPlaying.Count; i > 0; i--)
            {
                var nt = notesCurrentlyPlaying[0];
                nt.Stop();
                notesCurrentlyPlaying.RemoveAt(0);
            }
        }
        var n = AudioSources[sound][note];
        n.Play();
        Debug.Log($"playing {sound} {note}");
        if (!notesCurrentlyPlaying.Contains(n))
            notesCurrentlyPlaying.Add(n);
    }
    public void PlayMovementSound(int i)
    {
        var note = MovementAudioSources[i];
        note.Play();
        notesCurrentlyPlaying.Add(note);
    }

    public void PlayPuzzle1Sound(int i)
    {

        var note = Puzzle1AudioSources[i];
        note.Play();
        notesCurrentlyPlaying.Add(note);
    }
}
