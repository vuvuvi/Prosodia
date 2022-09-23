using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "So/IntVariable")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
    private int val;
    public int Value
    {
        get => val;
        set => Validate(value);
    }
    public UnityEvent ValueChanged = new UnityEvent();

    private void Validate(int newVal)
    {
        if (val != newVal)
        {
            val = newVal;
            ValueChanged.Invoke();
        }
    }
}
