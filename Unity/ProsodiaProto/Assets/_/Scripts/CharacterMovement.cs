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
    public NoteInfoProvider NoteInfoProvider;
    public NavMeshAgent AgentNavMesh;
    private CameraManager cameraManager;
    private ChangeAudioMixedVolume audioVolumeChanger;
    public float WaitingTimeStandUp;
    public Overlay Overlay;
    public AnimationTime waitWakeUp;
    public Tutorial Tutorial;

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
        waitWakeUp.Duration = WaitingTimeStandUp;
    }

    internal void StartPlaying()
    {
        animator.SetBool(isPlayingHash, true);
    }

    public void ToggleMovePlay()
    {
        isPlaying = !isPlaying;
        isInMovement &= !isPlaying;
        if (!isPlaying)
            animator.SetBool(isPlayingHash, false);
        cameraManager.SetCamera(IsPlaying);
        audioVolumeChanger.ChangeVolume();
    }

    public void Echolocation()
    {
        if (!isPlaying && !Iwalk && Time.time > WaitingTimeStandUp) //To Blocked Wake up animation
        {
            audioManager.PlayNote(0, "Move");

            if (!(effectPingPlayer.animate.State == StateAnime.STARTED))
            {
                effectPingPlayer.StartAnimation();

                for (int i = 0; i < Location.Locations.Count; i++)
                {
                    var loc = Location.Locations[i];
                    HiddeAllHighlights(1);
                    var colorId = i + ((i < 2) ? 0 : 1);
                    Color color = NoteInfoProvider.GetNoteColor(colorId);
                    PingLocation(i, color).transform.position = loc.transform.position;

                    Tutorial.RefreshText(1, color);
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

    public void HiddePingLocation()
    {
        foreach (var ping in pingLocations)
        {
            ping.animate.State = StateAnime.STOPED;
            ping.shader.SetFloat("_Transparency", 0);
            ping.toShow = false;
        }
    }

    public void MoveTo(int pos)
    {
        if (isInMovement && !Iwalk && Location.Locations.Count > pos && Location.Locations[pos].IsAvailable)
        {
            Tutorial.RefreshText(1, Color.blue);
            HiddePingLocation();
            audioManager.PlayNote(pos + 1, "Move");
            Location = Location.Locations[pos];
            Iwalk = true;
            animator.SetBool(isWalkingHash, true);
            AgentNavMesh.SetDestination(Location.transform.position);
            HiddeAllHighlights(0);
            Tutorial.RefreshText(2, Color.white);
        }
    }

    public bool CharacterReachedDestination()
    {
        return Iwalk && AgentNavMesh.remainingDistance != Mathf.Infinity && AgentNavMesh.pathStatus == NavMeshPathStatus.PathComplete && AgentNavMesh.remainingDistance == 0;
    }

    public void WakeUpFinish(float time)
    {
        Tutorial.RefreshText(0, NoteInfoProvider.GetNoteColor(2));
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
}