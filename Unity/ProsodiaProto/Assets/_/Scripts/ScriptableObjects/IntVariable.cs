using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "So/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int Value;
    public UnityEvent ValueChanged = new UnityEvent();
    private int oldValue;
    private void OnEnable()
    {
        oldValue = Value;
    }
    private void OnValidate()
    {
        if (oldValue != Value)
        {
            ValueChanged.Invoke();
            oldValue = Value;
        }
    }
}
