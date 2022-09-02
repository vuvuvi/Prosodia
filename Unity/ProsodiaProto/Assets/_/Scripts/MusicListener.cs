using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicListener : MelodyHolder
{
  private Melody melodyGoal
  {
    get { return Melody; }
    set { Melody = value; }
  }
  private Melody playerMelody;
  public UnityEvent GoalReached = new UnityEvent();

  private void Start()
  {

    var melodyGenerator = new MelodyGenerator();
    melodyGoal = melodyGenerator.GetNewMelody(MelodyLength);
  }
  private void OnTriggerEnter(Collider other)
  {
    
    var player = other.gameObject.GetComponent<PlayerMelodyManager>();
    if(player == null)
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

  private void UnsubscribeToPlayer(PlayerMelodyManager player)
  {
    playerMelody = null;
    player.MelodyChanged.RemoveListener(CompareMelodies);
  }

  private void SubscribeToPlayer(PlayerMelodyManager player)
  {
    playerMelody = player.CurrentMelody;
    player.MelodyChanged.AddListener(CompareMelodies);
  }

  private void CompareMelodies()
  {
    if(!melodyGoal.StartsWith(playerMelody))
    {
      playerMelody.Reset();
      return;
    }
    if(melodyGoal == playerMelody)
    {
      GoalReached.Invoke();
    }
  }
}
