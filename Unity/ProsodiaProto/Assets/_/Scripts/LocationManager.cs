using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
  public List<Location> locations;
  public List<Location> locationsArround;
  private List<Material> materials;
  public Material Red;
  public Material Blue;
  public Material Green;
  public Material Black;
  public Material Yellow;
  public Material White;
  public Boolean refreshIdLocations;

  public static readonly int MAX_LOCATIONS = 4;

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

  public void PlayerPing(Vector3 playerPosition, List<Location> locationsArround)
  {
    this.locationsArround = locationsArround;
    foreach (Location location in locations)
    {
      location.DistanceFromPlayer = Vector3.Distance(playerPosition, location.transform.position);
    }
    locations = locations.OrderBy(location => location.DistanceFromPlayer).ToList();
    ColorClosestLocations();
  }

  private void ColorClosestLocations()
  {
    for (int i = 0; i < this.locationsArround.Count; i++)
    {
      locationsArround[i].ChangeMaterial(materials[i]);
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

  public void PingLocation(Location location)
  {
    int index = this.locationsArround.IndexOf(location);

    if(index < 0)
    {
      this.locationsArround.Add(location);
      index = this.locationsArround.Count-1;
    }
    
    location.ChangeMaterial(materials[index]);
  } 

  public void RefreshIdLocation()
  {
    locations = FindObjectsOfType<MonoBehaviour>().OfType<Location>().ToList();
    
    for (int i = 0; i < locations.Count; i++)
    {
      locations[i].name = "Location " + i;
      locations[i].id = i;
      locations[i].transform.parent = null;
      locations[i].transform.parent = transform;
    }
  }

  private void OnDrawGizmosSelected()
  {
    if(refreshIdLocations)
    {  
      RefreshIdLocation();
      Debug.Log("Refresh ID locations");
      refreshIdLocations = false;
    }
  }

  public Location GetLocation(int id)
  {
    return locations.Find(location => location.id == id);
  }
}
