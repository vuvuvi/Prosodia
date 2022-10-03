using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using System;

public class CharacterMovement : MonoBehaviour
{
    LocationManager locationManager;
    private bool isInMovement;
    private bool isPlaying;
    private Vector3 movement;
    public EffectPingPlayer effectPingPlayer;
    private PlayerInput playerInput;
    public Location Location;
    public bool IsPlaying => isPlaying;
    public Keyboard keyboard;
    public TMPro.TMP_Text TextMode;
    public KeyCode[] KeysCodes = new KeyCode[4] { KeyCode.Q, KeyCode.S, KeyCode.F, KeyCode.G };
    private AudioManager audioManager;
    public bool Iwalk;
    public float MoveSpeed = 6;
    Animator animator;
    int isWalkingHash;
    int isPlayingHash;
    public Transform MeshContainer;
    private CameraManager cameraManager;
    void Start()
    {
        locationManager = FindObjectOfType<LocationManager>();
        playerInput = GetComponent<PlayerInput>();
        audioManager = GetComponentInChildren<AudioManager>();
        animator = GetComponentInChildren<Animator>();
        isPlayingHash = Animator.StringToHash("isPlaying");
        isWalkingHash = Animator.StringToHash("isWalking");
        transform.position = Location.transform.position;
        cameraManager = GetComponent<CameraManager>();
        cameraManager.SetCharacterTransform(MeshContainer);
    }

    internal void StartPlaying()
    {
        animator.SetBool(isPlayingHash, true);
    }

    public void ToggleMovePlay()
    {
        if (!isInMovement)
            isPlaying = !isPlaying;
        if (!isPlaying)
            animator.SetBool(isPlayingHash, false);
        cameraManager.ToggleCamera();
    }

    public void Echolocation()
    {
        if (!isPlaying && !Iwalk)
        {
            audioManager.PlayNote(0, "Move");
            effectPingPlayer.StartAnimation();
            Location.noteKeyboard.text = "";
            for (int i = 0; i < Location.Locations.Count; i++)
            {
                var loc = Location.Locations[i];
                loc.noteKeyboard.text = KeysCodes[i].ToString();
            }
            isInMovement = true;
        }
    }

    public void MoveTo(int pos)
    {
        if (isInMovement)
        {
            audioManager.PlayMovementSound(pos + 1);
            locationManager.UncolorLocationsPinged();
            Location = Location.Locations[pos];
            locationManager.LocationsArround = new List<Location>();
            Iwalk = true;
            animator.SetBool(isWalkingHash, true);
            MeshContainer.LookAt(Location.transform, Vector3.up);
        }
    }

    void Update()
    {
        var locPos = Location.transform.position;
        var playerPos = transform.position;
        locPos.y = 0;
        playerPos.y = 0;
        var distanceLeft = Vector3.Distance(locPos, playerPos);
        if (distanceLeft > 0.2f && movement.sqrMagnitude > 0.1f)
        {
            var frameMovement = movement.normalized * Time.deltaTime * MoveSpeed;
            transform.position += frameMovement;
        }
        else
        {
            if (distanceLeft < 0.2f)
            {
                animator.SetBool(isWalkingHash, false);
                Iwalk = false;
            }

            if (isInMovement)
            {
                movement = Location.transform.position - transform.position;
                //movement.y = 0;
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