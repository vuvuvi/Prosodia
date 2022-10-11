using Cinemachine;
using System;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera Camera;
    public CinemachineVirtualCamera CharacterVirtualCam;
    private CinemachineVirtualCamera puzzleVirtualCam;
    private PerspectiveSwitcher perspectiveSwitcher;
    private bool isOrtho;
    public Transform CharacterMesh;


    private void Start()
    {
        var allVirtualCamera = FindObjectsOfType<CinemachineVirtualCamera>().ToList();
        allVirtualCamera.ForEach(vc => vc.enabled = false);
        perspectiveSwitcher = Camera.gameObject.GetComponent<PerspectiveSwitcher>();
        ResetCamera();
    }

    private void OnTriggerEnter(Collider other)
    {
        var virtualCam = other.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        if (virtualCam == null)
            return;
        puzzleVirtualCam = virtualCam;
    }

    private void OnTriggerExit(Collider other)
    {
        var virtualCam = other.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        if (virtualCam == null)
            return;
        ResetCamera();
    }

    private void ResetCamera()
    {
        SwitchToOrtho();
        puzzleVirtualCam = null;
    }

    private void SwitchToThirdPerson()
    {
        if (puzzleVirtualCam == null)
            return;
        puzzleVirtualCam.enabled = true;
        CharacterVirtualCam.enabled = false;
        perspectiveSwitcher.SetPerspective();
        isOrtho = false;
    }
    private void SwitchToOrtho()
    {
        if (puzzleVirtualCam != null)
            puzzleVirtualCam.enabled = false;
        CharacterVirtualCam.enabled = true;
        perspectiveSwitcher.SetOrthographic();
        isOrtho = true;
        CharacterMesh.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        if (!isOrtho)
            CharacterMesh.rotation = Quaternion.LookRotation(Camera.transform.forward, Vector3.up);
    }

    internal void SetCamera(bool needPuzzleView)
    {
        if (needPuzzleView)
            SwitchToThirdPerson();
        else
            SwitchToOrtho();
    }
}
