using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyGenerator : ScriptableObject
{
  public List<int> AvailableNotes = new List<int>() { 0,1,2,3,4,5,6,7,8,9};
  public Melody GetNewMelody(int length)
  {
    var m = new Melody();
    for (int i = 0; i < length; i++)
      m.AddNote(AvailableNotes[Random.Range(0, AvailableNotes.Count)]);
    return m;
  }
}
