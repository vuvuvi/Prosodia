using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
  public float DistanceFromPlayer;
  private MeshRenderer meshRenderer;
  public PongEffect effect;
  public List<Location> locations;
  public int id;
  
  private void Start()
  {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  internal void ChangeMaterial(Material material)
  {
    meshRenderer.material = material;
    PongEffect pongEffect = Instantiate(effect, transform);
    pongEffect.StartAnimation();
    Destroy(pongEffect.gameObject, effect.duration);
  }

  private void OnDrawGizmosSelected()
  {
    foreach (var location in locations)
    {
      if(location)
      {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, location.transform.position);
      }
    }
  }
}
