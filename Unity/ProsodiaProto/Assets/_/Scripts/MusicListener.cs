using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicListener : MelodyHolder
{
  protected Melody playerMelody;
  public UnityEvent GoalReached = new UnityEvent();

  protected void Start()
  {
    var melodyGenerator = new MelodyGenerator();
    Melody = melodyGenerator.GetNewMelody(MelodyLength);
  }
  private void OnTriggerEnter(Collider other)
  {
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
      playerMelody.Reset();
      return;
    }
    if (Melody == playerMelody)
    {
      GoalReached.Invoke();
    }
  }
}
