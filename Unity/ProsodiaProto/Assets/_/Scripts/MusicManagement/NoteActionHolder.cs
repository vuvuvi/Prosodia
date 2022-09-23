using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

public abstract class ActionHolder : MonoBehaviour
{
    public PuzzleOnMelody Puzzle;
    public Action Action;

    public List<ICanReachGoal> Triggers { get; private set; }

    protected virtual void OnEnable()
    {
        FindTrigger();
        CreateAction();
        foreach (var t in Triggers)
        {
            t.GoalReached.AddListener(Action.Invoke);
        }
    }

    private void FindTrigger()
    {
        Triggers = new List<ICanReachGoal>();
        if (Puzzle != null)
            Triggers.Add(Puzzle);
        else
            Triggers = GetComponents<MonoBehaviour>().OfType<ICanReachGoal>().ToList();
    }

    protected abstract void CreateAction();
}
