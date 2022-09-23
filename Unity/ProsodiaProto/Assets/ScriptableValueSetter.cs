using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableValueSetter : ScriptableObject
{
    public IntVariable NumberOfStonePuzzleSolved;
    private void OnEnable()
    {
        NumberOfStonePuzzleSolved.Value = 0;
    }
}
