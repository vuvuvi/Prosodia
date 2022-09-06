using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
  public List<Location> Locations;
  public List<Location> LocationsArround;
  public List<Material> Materials;
  public Material DefaultMatetrial;
  public Boolean RefreshIdLocations;

  public static readonly int MAX_LOCATIONS = 4;

  void Start()
  {
    Locations = FindObjectsOfType<MonoBehaviour>().OfType<Location>().ToList();
    RefreshIdLocation();
  }

  public void UncolorLocationsPinged()
  {
    foreach (Location location in LocationsArround)
    {
      location.ChangeMaterial(DefaultMatetrial);
      location.noteKeyboard.text = "";
      location.noteKeyboard.gameObject.SetActive(false);
    }
  }

  public void PingLocation(Location location, string note)
  {
    int index = this.LocationsArround.IndexOf(location);

    if(index < 0)
    {
      this.LocationsArround.Add(location);
      index = this.LocationsArround.Count-1;
    }
    
    location.PingLocation(Materials[index], note);
  } 

  public void RefreshIdLocation()
  {
    Locations = FindObjectsOfType<MonoBehaviour>().OfType<Location>().ToList();
    
    for (int i = 0; i < Locations.Count; i++)
    {
      Locations[i].name = "Location " + i;
      Locations[i].Id = i;
      Locations[i].transform.parent = null;
      Locations[i].transform.parent = transform;
    }
  }

  private void OnDrawGizmosSelected()
  {
    if(RefreshIdLocations)
    {  
      RefreshIdLocation();
      Debug.Log("Refresh ID locations");
      RefreshIdLocations = false;
    }
  }

  public Location GetLocation(int id)
  {
    return Locations.Find(location => location.Id == id);
  }
}
