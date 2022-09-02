using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="So/Tools/NoteInfoProvider")]
public class NoteInfoProvider : ScriptableObject
{
  public List<Color> Colors = new List<Color>();
  public List<Sprite> Sprites = new List<Sprite>();
  public Color GetNoteColor(int note)
  {
    return Colors[note];
  }
  public Sprite GetNoteSymbol(int note)
  {
    return Sprites[note];
  }
}
