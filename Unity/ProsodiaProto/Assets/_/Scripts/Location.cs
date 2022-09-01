using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        Vector3 orientation = location.transform.position-transform.position;
        Vector3 pos = transform.position + orientation;
        Gizmos.DrawLine(transform.position, location.transform.position);
        Gizmos.DrawMesh(MeshUtils.Triangle(.25f), pos - orientation.normalized * .25f , Quaternion.LookRotation(orientation));
      }
    }
  }

  private void OnDrawGizmos()
  {
    GUIStyle gUIStyle = new GUIStyle();
    gUIStyle.fontSize = 20;
    gUIStyle.normal.textColor = Color.white;
    //Handles.Label(transform.position, id.ToString(), gUIStyle);
  }
}
