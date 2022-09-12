using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

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
    public KeyCode[] KeysCodes = new KeyCode[4] { KeyCode.Q, KeyCode.S, KeyCode.F, KeyCode.G };
    private AudioManager audioManager;
    public bool Iwalk;
    public float MoveSpeed = 6;
    void Start()
    {
        locationManager = FindObjectOfType<LocationManager>();
        playerInput = GetComponent<PlayerInput>();
        audioManager = GetComponentInChildren<AudioManager>();
        keyboard = new Keyboard();
        keyboard.Enable();
        keyboard.PlayerMusic.ToggleMovePlay.performed += ctx => { Debug.Log(ctx.ReadValueAsObject()); };
    }

    void OnToggleMovePlay()
    {
        if (!isInMovement)
            isPlaying = !isPlaying;

        TextMode.text = isPlaying ? "Piano Mode" : "Moving Mode";
    }

    void Update()
    {
        var locPos = Location.transform.position;
        var playerPos = transform.position;
        locPos.y = 0;
        playerPos.y = 0;
        if (Vector3.Distance(locPos, playerPos) > 0.2f)
        {
            var frameMovement = movement.normalized * Time.deltaTime * MoveSpeed;
            transform.position += frameMovement;
        }
        else
        {
            Iwalk = false;

            if (!isPlaying && Input.GetKeyDown(KeyCode.D))
            {
                audioManager.PlayNote(0, "Move");
                effectPing.StartAnimation();
                Location.noteKeyboard.text = "";
                for (int i = 0; i < Location.Locations.Count; i++)
                {
                    var loc = Location.Locations[i];
                    loc.noteKeyboard.text = KeysCodes[i].ToString();
                }
                isInMovement = true;
                Location.transform.position = transform.position;
            }
            if (isInMovement)
            {
                for (int i = 0; i < Location.Locations.Count; i++)
                {
                    if (Input.GetKeyDown(KeysCodes[i]))
                    {
                        audioManager.PlayMovementSound(i + 1);
                        locationManager.UncolorLocationsPinged();
                        Location = Location.Locations[i];
                        locationManager.LocationsArround = new List<Location>();
                        Iwalk = true;
                    }
                }

                movement = Location.transform.position - transform.position;
                movement.y = 0;
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
        if (loc && Location.Locations.Contains(loc))
        {
            locationManager.PingLocation(loc, KeysCodes[locationManager.LocationsArround.Count].ToString());
        }
    }
}
