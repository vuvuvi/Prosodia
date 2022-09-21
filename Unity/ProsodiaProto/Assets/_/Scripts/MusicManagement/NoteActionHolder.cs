using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoteActionHolder : MonoBehaviour
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

public abstract class PuzzleActionHolder : MonoBehaviour
{
    public PuzzleOnMelody Puzzle;
    public Action Action;
    protected virtual void OnEnable()
    {
        CreateAction();
        Puzzle.GoalReached.AddListener(Action.Invoke);
    }
    protected abstract void CreateAction();
}
