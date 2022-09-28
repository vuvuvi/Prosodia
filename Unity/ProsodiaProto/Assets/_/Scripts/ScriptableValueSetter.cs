using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableValueSetter : MonoBehaviour
{
    public IntVariable NumberOfStonePuzzleSolved;
    private void OnEnable()
    {
        NumberOfStonePuzzleSolved.Value = 0;
    }
}
