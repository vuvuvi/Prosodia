using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicListener : MelodyHolder, ICanReachGoal
{
    protected Melody playerMelody;
    public UnityEvent Event = new UnityEvent();
    public GameObject ItemsContainer;
    public List<int> FixedMelody;
    public bool RandomMelody;

    public UnityEvent GoalReached => Event;

    protected void Start()
    {
        var melodyGenerator = new MelodyGenerator();
        Melody = melodyGenerator.GetNewMelody(MelodyLength);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        var player = other.gameObject.GetComponent<PlayerMelodyManager>();
        if (player == null)
            return;
        SubscribeToPlayer(player);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerMelodyManager>();
        if (player == null)
            return;
        UnsubscribeToPlayer(player);
    }

    protected virtual void UnsubscribeToPlayer(PlayerMelodyManager player)
    {
        playerMelody.Reset();
        playerMelody = null;
        player.MelodyChanged.RemoveListener(CompareMelodies);
    }

    protected virtual void SubscribeToPlayer(PlayerMelodyManager player)
    {
        playerMelody = player.CurrentMelody;
        player.MelodyChanged.AddListener(CompareMelodies);
    }

    private void CompareMelodies()
    {
        if (!Melody.StartsWith(playerMelody))
        {
            var lastNote = playerMelody.Notes[playerMelody.Notes.Count - 1];
            playerMelody.Reset();
            if(lastNote == Melody.Notes[0])
            {
                playerMelody.AddNote(lastNote);
            }
            return;
        }
        if (Melody == playerMelody)
        {
            Event.Invoke();
        }
    }
}
