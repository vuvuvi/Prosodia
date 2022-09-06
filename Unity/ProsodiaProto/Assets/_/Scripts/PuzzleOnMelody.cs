using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleOnMelody : MusicListener
{
  public GameObject ItemsContainer;
  public List<NoteListener> NoteListeners;

  protected new void Start()
  {
    NoteListeners = ItemsContainer.GetComponentsInChildren<NoteListener>().ToList();
    MelodyLength = NoteListeners.Count;
    base.Start();
    for (int i = 0; i < NoteListeners.Count; i++)
    {
      var child = NoteListeners[i];
      child.NotePitch = Melody.Notes[i];
    }
  }
  protected override void SubscribeToPlayer(PlayerMelodyManager player)
  {
    base.SubscribeToPlayer(player);
    player.MelodyChanged.AddListener(ValidateMelody);

  }
  protected override void UnsubscribeToPlayer(PlayerMelodyManager player)
  {
    base.UnsubscribeToPlayer(player);
    player.MelodyChanged.RemoveListener(ValidateMelody);

  }
  private void ValidateMelody()
  {
    for (int i = 0; i < Melody.Notes.Count; i++)
    {
      NoteListeners[i].IsValid = playerMelody.Notes.Count > i && playerMelody.Notes[i] == Melody.Notes[i];
    }
  }
}
