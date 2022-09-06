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
  public bool IsPlaying => isPlaying;
  public Keyboard keyboard;
  public TMPro.TMP_Text TextMode;
  public KeyCode [] KeysCodes = new KeyCode[4]{KeyCode.Q, KeyCode.S, KeyCode.F, KeyCode.G};
  void Start()
  {
    locationManager = FindObjectOfType<LocationManager>();
    playerInput = GetComponent<PlayerInput>();
    keyboard = new Keyboard();
    keyboard.Enable();
    //keyboard.PlayerMusic.ToggleMovePlay.performed += ctx => {Debug.Log(ctx.ReadValueAsObject());};
  }

  void OnToggleMovePlay()
  {
    if (!isInMovement)
      isPlaying = !isPlaying;

    TextMode.text = isPlaying ? "Piano Mode" : "Moving Mode";
  }

  void Update()
  {
    if (Vector3.Distance(Location.transform.position, transform.position) > 0.3)
    {
      var frameMovement = movement.normalized * Time.deltaTime * 3;
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
        for (int i = 0; i < Location.Locations.Count; i++)
        {
          if(Input.GetKeyDown(KeysCodes[i]))
          {
            locationManager.UncolorLocationsPinged();
            Location = Location.Locations[i];
            locationManager.LocationsArround = new List<Location>();
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
    if(loc && Location.Locations.Contains(loc))
    {
      locationManager.PingLocation(loc, KeysCodes[locationManager.LocationsArround.Count].ToString());
    }
  }
}