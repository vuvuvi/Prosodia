using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
  LocationManager locationManager;
  private bool isInMovement;
  private bool isPlaying;
  private Vector3 targetLocation;
  private Vector3 movement;
  public PongEffect effectPing;
  private PlayerInput playerInput;

  void Start()
  {
    locationManager = FindObjectOfType<LocationManager>();
    playerInput = GetComponent<PlayerInput>();
    targetLocation = transform.position;
  }

  void OnToggleMovePlay()
  {
    if (!isInMovement)
      isPlaying = !isPlaying;
  }
  void Update()
  {
    if (Vector3.Distance(targetLocation, transform.position) > 0.3)
    {
      var frameMovement = movement.normalized * Time.deltaTime;
      transform.position += frameMovement;
      Camera.main.transform.position += frameMovement;
    }
    else
    {
      if (!isPlaying && Input.GetKeyDown(KeyCode.D))
      {
        locationManager.PlayerPing(transform.position);
        effectPing.StartAnimation();
        isInMovement = true;
        targetLocation = transform.position;
      }
      if (isInMovement)
      {
        if (Input.GetKeyDown(KeyCode.Q))
        {
          targetLocation = locationManager.GetDestinationOfLocation(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
          targetLocation = locationManager.GetDestinationOfLocation(2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
          targetLocation = locationManager.GetDestinationOfLocation(3);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
          targetLocation = locationManager.GetDestinationOfLocation(4);
        }
        movement = targetLocation - transform.position;
        if (movement.sqrMagnitude > 0.1)
        {
          isInMovement = false;
        }
      }

    }
  }
}
