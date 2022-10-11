using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class Location : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float DistanceFromPlayer;
    public List<Location> Locations;
    public List<float> Distances;
    public int Id;
    public bool IsAvailable = true;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    internal void PingLocation(Material material, string note)
    {
        ChangeMaterial(material);
    }

    public void ChangeMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var location in Locations)
        {
            if (location)
            {
                Gizmos.color = Color.red;
                Vector3 orientation = location.transform.position - transform.position;
                Vector3 pos = transform.position + orientation;
                Gizmos.DrawLine(transform.position, location.transform.position);
                Gizmos.DrawMesh(MeshUtils.Triangle(.25f), pos - orientation.normalized * .25f, Quaternion.LookRotation(orientation));
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
        if(Application.isPlaying) return;
        RefreshDistances();
        SetId();
        if (this.Locations.Count > LocationManager.MAX_LOCATIONS)
        {
            Debug.LogWarning("You can't add more location");
            this.Locations.RemoveAt(this.Locations.Count - 1);
        }

        for (int i = 0; i < Locations.Count; i++)
        {
            Location location = Locations[i];

            if (location && !location.Locations.Contains(this))
            {
                Debug.Log($" {name} is connect to {location.name}. But {location.name} is not connect to {name}. I will connect them.");
                if (location.Locations.Count < 4)
                {
                    location.Locations.Add(this);
                }
                else
                {
                    Debug.LogWarning($"I can't add more location on {name}, {location.name} is sill missing");
                }
            }
        }
    }

    public void SetId()
    {
        Id = FindObjectOfType<LocationManager>().AddLocation(this);
        name = "Location " + Id;
    }

    private void RefreshDistances()
    {
        Distances = new List<float>();

        foreach (var location in Locations)
        {
            if (location)
            {
                Distances.Add(Vector3.Distance(transform.position, location.transform.position));
            }
        }
    }
}