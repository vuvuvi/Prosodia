using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
  public float DistanceFromPlayer;
  private MeshRenderer meshRenderer;
  public PongEffect effect;
  
  private void Start()
  {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  internal void ChangeMaterial(Material material)
  {
    meshRenderer.material = material;
    Destroy(Instantiate(effect, transform).gameObject, effect.duration);
  }
}
