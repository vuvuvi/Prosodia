using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public bool IsPlaying => isPlaying;
    LocationManager locationManager;
    private bool isInMovement;
    private bool isPlaying;
    public EffectPingPlayer effectPingPlayer;
    public Location Location;
    public Keyboard keyboard;
    public TMPro.TMP_Text TextMode;
    public KeyCode[] KeysCodes = new KeyCode[4] { KeyCode.Q, KeyCode.S, KeyCode.F, KeyCode.G };
    public PingLocation[] pingLocations;
    private AudioManager audioManager;
    public bool Iwalk;
    public float MoveSpeed = 6;
    private Animator animator;
    private int isWalkingHash;
    private int isPlayingHash;
    public Transform MeshContainer;
    public NoteInfoProvider NoteInfoProvider;
    public NavMeshAgent AgentNavMesh;
    private CameraManager cameraManager;
    private ChangeAudioMixedVolume audioVolumeChanger;

    void Start()
    {
        audioVolumeChanger = FindObjectOfType<ChangeAudioMixedVolume>();
        locationManager = FindObjectOfType<LocationManager>();
        audioManager = FindObjectOfType<AudioManager>();
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
        audioVolumeChanger.ChangeVolume();
    }

    public void Echolocation()
    {
        if (!isPlaying && !Iwalk)
        {
            audioManager.PlayNote(0, "Move");

            if (!(effectPingPlayer.animate.State == StateAnime.STARTED))
            {
                effectPingPlayer.StartAnimation();

                Location.noteKeyboard.text = "";
                for (int i = 0; i < Location.Locations.Count; i++)
                {
                    var loc = Location.Locations[i];
                    loc.noteKeyboard.text = KeysCodes[i].ToString();
                    PuzzleOnMelody puzzleOnMelody = loc.GetComponent<PuzzleOnMelody>();
                    HiddeAllHighlights(1);
                    Color color = NoteInfoProvider.GetNoteColor(i);
                    PingLocation(i, color).transform.position = loc.transform.position;
                }
                isInMovement = true;
            }
        }
    }

    public void HiddeAllHighlights(float value)
    {
        Highlight highlight = FindObjectOfType<Highlight>();
        highlight.material.SetFloat("_transparency", value);
    }

    public PingLocation PingLocation(int index, Color color)
    {
        PingLocation pingLocation = pingLocations[index];
        pingLocation.SetColor(color);
        pingLocation.StartAnimation();
        return pingLocation;
    }

    public void MoveTo(int pos)
    {
        if (isInMovement && Location.Locations[pos].IsAvailable)
        {
            audioManager.PlayNote(pos + 1, "Move");
            locationManager.UncolorLocationsPinged();
            Location = Location.Locations[pos];
            locationManager.LocationsArround = new List<Location>();
            Iwalk = true;
            animator.SetBool(isWalkingHash, true);
            AgentNavMesh.SetDestination(Location.transform.position);
            HiddeAllHighlights(0);
        }
    }

    public bool CharacterReachedDestination()
    {
        return Iwalk && AgentNavMesh.remainingDistance != Mathf.Infinity && AgentNavMesh.pathStatus == NavMeshPathStatus.PathComplete && AgentNavMesh.remainingDistance == 0;
    }

    void Update()
    {
        if (CharacterReachedDestination())
        {
            animator.SetBool(isWalkingHash, false);
            Iwalk = false;
            isInMovement = false;
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