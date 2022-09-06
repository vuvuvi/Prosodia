using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Location : MonoBehaviour
{
  private MeshRenderer meshRenderer;
  public float DistanceFromPlayer;
  public PingEffect Effect;
  public List<Location> Locations;
  public List<float> Distances;
  public int Id;
  public TMPro.TMP_Text noteKeyboard;
  
  private void Start()
  {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  internal void PingLocation(Material material, string note)
  {
    ChangeMaterial(material);
    Effect.StartAnimation();
    noteKeyboard.text = note;
    noteKeyboard.gameObject.SetActive(true);
  }

  public void ChangeMaterial(Material material)
  {
    meshRenderer.material = material;
  }

  private void OnDrawGizmosSelected()
  {
    foreach (var location in Locations)
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
#if UNITY_EDITOR
    Handles.Label(transform.position, Id.ToString(), gUIStyle);
#endif
  }

  private void OnValidate()
  {
    RefreshDistances();
    if(this.Locations.Count > LocationManager.MAX_LOCATIONS)
    {
      Debug.LogWarning("You can't add more location");
      this.Locations.RemoveAt(this.Locations.Count-1);
    }
  }

  private void RefreshDistances()
  {
    Distances = new List<float>();

    foreach (var location in Locations)
    {
      if(location)
      {
        Distances.Add(Vector3.Distance(transform.position, location.transform.position));
      }
    }
  }
}
