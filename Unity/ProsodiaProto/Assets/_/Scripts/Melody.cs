using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Melody : IEquatable<Melody>
{
  public List<int> Notes = new List<int>() ;

  public Melody() { }
  public Melody(Melody melody)
  {
    for (int i = 0; i < melody.Notes.Count; i++)
    {
      AddNote(melody.Notes[i]);
    }
  }
  public Melody(List<int> notes)
  {
    Notes = notes;
  }
  public void AddNote(int note)
  {
    Notes.Add(note);
  }

  public bool StartsWith(Melody other)
  {
    var maxLength = MathF.Min(Notes.Count, other.Notes.Count);
    for(int i = 0; i < maxLength; i++)
    {
      if (Notes[i] != other.Notes[i])
        return false;
    }
    return true;
  }
  public bool Equals(Melody other)
  {
    if(other.Notes.Count != Notes.Count)
      return false;
    return StartsWith(other);
  }

  public override bool Equals(object obj)
  {
    var m = obj as Melody;
    if (m == null)
      return false;
    return Equals(m);
  }

  public override int GetHashCode()
  {
    return base.GetHashCode();
  }

  public override string ToString()
  {
    string retVal = "";
    for (int i = 0; i < Notes.Count; i++)
    {
      retVal += $"{Notes[i]} ";
    }
    return retVal;
  }

  internal void Reset()
  {
    Notes.Clear();
  }

  public static bool operator == (Melody m1, Melody m2)
  {
    return m1.Equals(m2);
  }
  public static bool operator != (Melody m1, Melody m2)
  {
    return !m1.Equals(m2);
  }
}
