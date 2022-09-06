using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
  LocationManager locationManager;
  private bool isInMovement;
  private bool isPlaying;
  private Vector3 movement;
  public PingEffect effectPing;
  private PlayerInput playerInput;
  public Location Location;

  void Start()
  {
    locationManager = FindObjectOfType<LocationManager>();
    playerInput = GetComponent<PlayerInput>();
  }

  void OnToggleMovePlay()
  {
    if (!isInMovement)
      isPlaying = !isPlaying;
  }
  void Update()
  {
    if (Vector3.Distance(Location.transform.position, transform.position) > 0.3)
    {
      var frameMovement = movement.normalized * Time.deltaTime;
      transform.position += frameMovement;
      Camera.main.transform.position += frameMovement;
    }
    else
    {
      if (!isPlaying && Input.GetKeyDown(KeyCode.D))
      {
        effectPing.StartAnimation();
        isInMovement = true;
        Location.transform.position = transform.position;
      }
      if (isInMovement)
      {
        KeyCode [] keysCodes = new KeyCode[4]{KeyCode.Q, KeyCode.S, KeyCode.F, KeyCode.G};
        
        for (int i = 0; i < Location.locations.Count; i++)
        {
          if(Input.GetKeyDown(keysCodes[i]))
          {
            Location = Location.locations[i];
            locationManager.LocationsArround = new List<Location>();
            locationManager.GetDestinationOfLocation(0);
          }
        }
        
        movement = Location.transform.position - transform.position;
        if (movement.sqrMagnitude > 0.1)
        {
          isInMovement = false;
        }
      }

    }
  }

  private void OnTriggerEnter(Collider other)
  {
    Location loc = other.GetComponent<Location>();
    if(loc && Location.locations.Contains(loc))
    {
      locationManager.PingLocation(loc);
    }
  }
}
