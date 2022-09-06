using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScriptableObjectsInjector : MonoBehaviour
{
  public NoteInfoProvider NoteInfoProvider;
  void Start()
  {
    FindObjectsOfType<LightUpAction>().ToList().ForEach(lua => lua.NoteInfoProvider = NoteInfoProvider);
  }
}