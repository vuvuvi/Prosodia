using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Location : MonoBehaviour
{
  public float DistanceFromPlayer;
  private MeshRenderer meshRenderer;
  public PingEffect effect;
  public List<Location> locations;
  public List<float> distances;
  public int id;
  
  private void Start()
  {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  internal void PingLocation(Material material)
  {
    ChangeMaterial(material);
    PingEffect pongEffect = Instantiate(effect, transform);
    pongEffect.StartAnimation();
    Destroy(pongEffect.gameObject, effect.duration);
  }

  public void ChangeMaterial(Material material)
  {
    meshRenderer.material = material;
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
    RefreshDistances();
    GUIStyle gUIStyle = new GUIStyle();
    gUIStyle.fontSize = 20;
    gUIStyle.normal.textColor = Color.white;
    //Handles.Label(transform.position, id.ToString(), gUIStyle);
  }

  private void OnValidate()
  {
    RefreshDistances();
    if(this.locations.Count > LocationManager.MAX_LOCATIONS)
    {
      Debug.LogWarning("You can't add more location");
      this.locations.RemoveAt(this.locations.Count-1);
    }
  }

  private void RefreshDistances()
  {
    distances = new List<float>();

    foreach (var location in locations)
    {
      if(location)
      {
        distances.Add(Vector3.Distance(transform.position, location.transform.position));
      }
    }
#if UNITY_EDITOR
    Handles.Label(transform.position, id.ToString(), gUIStyle);
#endif
  }
}
