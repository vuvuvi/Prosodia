using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerMelodyManager : MonoBehaviour
{
    public Melody CurrentMelody;
    public UnityEvent MelodyChanged;

    public UnityEvent<int> NotePlayed;
    private CharacterMovement characterController;
    private AudioManager audioManager;

    public string SoundName { get; private set; } = "Puzzle1";


    private void Start()
    {
        audioManager = GetComponentInChildren<AudioManager>();
        CurrentMelody = new Melody();
        MelodyChanged = new UnityEvent();
        characterController = GetComponent<CharacterMovement>();
        SoundName = audioManager.GetSoundName(0);
        Debug.Log(SoundName);
    }
    public void AddNote(int note)
    {
        if (!characterController.IsPlaying)
            return;

        audioManager.PlayNote(note, SoundName, true);
        CurrentMelody.AddNote(note);
        MelodyChanged.Invoke();
        NotePlayed.Invoke(note);
    }
    public void SetSound(string sound)
    {
        SoundName = sound;
    }
    public void Reset()
    {
        CurrentMelody.Reset();
    }
}