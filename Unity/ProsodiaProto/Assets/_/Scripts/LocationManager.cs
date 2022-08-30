using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
  List<Location> locations;
  private List<Material> materials;
  public Material Red;
  public Material Blue;
  public Material Green;
  public Material Black;
  public Material Yellow;
  public Material White;
  
  void Start()
  {
    locations = FindObjectsOfType<MonoBehaviour>().OfType<Location>().ToList();
    materials = new List<Material>();
    materials.Add(Black);
    materials.Add(Red);
    materials.Add(Blue);
    materials.Add(Green);
    materials.Add(Yellow);
    materials.Add(White);
  }

  public void PlayerPing(Vector3 playerPosition)
  {
    foreach (Location location in locations)
    {
      location.DistanceFromPlayer = Vector3.Distance(playerPosition, location.transform.position);
    }
    locations = locations.OrderBy(location => location.DistanceFromPlayer).ToList();
    ColorClosestLocations();
  }

  private void ColorClosestLocations()
  {
    for (int i = 0; i < materials.Count-1; i++)
    {
      locations[i].ChangeMaterial(materials[i]);
    }
  }
  private void UncolorLocations()
  {
    foreach (Location location in locations)
      location.ChangeMaterial(White);
  }
  public Vector3 GetDestinationOfLocation(int positionIndex)
  {
    UncolorLocations();
    return locations[positionIndex].transform.position;
  }
}
