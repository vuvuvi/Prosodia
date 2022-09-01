using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="So/Actions/LightUp")]
public class LightUpAction : ActionHolder
{
  private void OnEnable()
  {
    Action = LightUp;
  }
  private void LightUp(GameObject go, int note)
  {
    Debug.Log($"{go.name} lights up for note{note}");
  }
  
}
