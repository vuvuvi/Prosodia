using UnityEngine;
using UnityEngine.Events;

public class CompareValuesComponent : MonoBehaviour, ICanReachGoal
{
    public IntVariable Value;
    public IntVariable Goal;

    public UnityEvent Event = new UnityEvent();
    public UnityEvent GoalReached => Event;

    void Start()
    {
        CompareValues();
        Value.ValueChanged.AddListener(CompareValues);
        Goal.ValueChanged.AddListener(CompareValues);
    }

    private void CompareValues()
    {
        if (Value.Value == Goal.Value)
        {
            GoalReached.Invoke();
        }
    }
}
