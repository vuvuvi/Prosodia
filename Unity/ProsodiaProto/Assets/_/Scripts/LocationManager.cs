using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
  public List<Location> Locations;
  public Boolean RefreshIdLocations;
  private Boolean refreshIdLocations;

  public static readonly int MAX_LOCATIONS = 4;

  void Start()
  {
    Locations = FindObjectsOfType<Location>().ToList();
    RefreshIdLocation();
  }

  public void RefreshIdLocation()
  {
    Locations = FindObjectsOfType<Location>().ToList();
    
    for (int i = 0; i < Locations.Count; i++)
    {
      Locations[i].name = "Location " + i;
      Locations[i].Id = i;
    }
  }

  public int AddLocation(Location loc)
  {
    if(Locations != null)
    {
      if(Locations.Contains(loc))
      {
        Debug.LogWarning("Locations already added");
      }
      else
      {
        Locations.Add(loc);
      }

      return Locations.IndexOf(loc);
    }
    else
    {
      Debug.LogWarning("Locations null why ?");
      return -1;
    }    
  }

  public Location GetLocation(int id)
  {
    return Locations.Find(location => location.Id == id);
  }

  private void OnDrawGizmosSelected()
  {
    if(RefreshIdLocations && !refreshIdLocations)
    {
      RefreshIdLocation();
      refreshIdLocations = true;
      StartCoroutine(WaitRefresh());
    }
  }

  private IEnumerator WaitRefresh()
  {
    yield return new WaitForSeconds(.1f);
    Debug.Log("Refresh ID locations");
    RefreshIdLocations = refreshIdLocations = false;
  }
}
