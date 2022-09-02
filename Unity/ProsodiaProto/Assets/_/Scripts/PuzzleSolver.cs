using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
  private Melody melodyToSolve;
  public bool IsSolvingPuzzle;
  public void SetMelody(Melody melody)
  {
    melodyToSolve = melody;
  }
  public bool IsMelodyRight(Melody melody)
  {
    return melodyToSolve.StartsWith(melody);
  }
}
