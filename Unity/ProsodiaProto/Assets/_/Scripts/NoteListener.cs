using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteListener : MonoBehaviour
{
  public int NotePitch;
  public ActionHolder Reaction;

  private void Start()
  {
    FindObjectOfType<PlayerMelodyManager>().NotePlayed.AddListener(ReactToNote);
  }

  private void ReactToNote(int note)
  {
    if (note != NotePitch)
    {
      return;
    }
    Act();
  }

  private void Act()
  {
    if (Reaction != null && Reaction.Action != null)
      Reaction.Action(gameObject, NotePitch);
  }
}
