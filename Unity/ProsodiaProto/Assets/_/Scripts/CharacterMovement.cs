using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
  LocationManager locationManager;
  private bool isMoving;
  private Vector3 targetLocation;
  private Vector3 movement;

  void Start()
  {
    locationManager = FindObjectOfType<LocationManager>();
    targetLocation = transform.position;
  }

  void Update()
  {
    if (Vector3.Distance(targetLocation, transform.position) > 0.3)
    {
      var frameMovement = movement.normalized * Time.deltaTime;
      transform.position += frameMovement;
      Camera.main.transform.position += frameMovement;
      float value = 0.5f;
      Quaternion.Euler(0, -90 + 180 * value, 80 * 2 * (0.5f - Mathf.Abs(-0.5f + value)));
    }
    else
    {
      if (Input.GetKeyDown(KeyCode.D))
      {
        locationManager.PlayerPing(transform.position);
        isMoving = true;
        targetLocation = transform.position;
      }
      if (isMoving)
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
          isMoving = false;
        }
      }

    }
  }
}
