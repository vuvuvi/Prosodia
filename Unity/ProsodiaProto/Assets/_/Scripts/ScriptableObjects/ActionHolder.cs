using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionHolder : MonoBehaviour
{
  public NoteInfoProvider NoteInfoProvider;
  public Action<int> Action;
  private bool isValid;

  public bool IsValid
  {
    get => isValid;
    set
    {
      isValid = value;
      Validate();
    }
  }

  protected abstract void Validate();

}
