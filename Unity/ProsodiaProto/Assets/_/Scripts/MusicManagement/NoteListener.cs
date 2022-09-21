using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteListener : MonoBehaviour
{
  public int NotePitch;
  private List<NoteActionHolder> reactions;
  private bool isValid;
  public bool IsValid
  {

    get => isValid;
    set
    {
      if (isValid != value)
      {
        isValid = value;
        reactions.ForEach(a => a.IsValid = isValid);
      }
    }
  }

  private void Start()
  {
    reactions = GetComponents<NoteActionHolder>().ToList();
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
    if (reactions != null)
      foreach (var action in reactions)
        action.Action(NotePitch);
  }
}
