using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera Camera;
    public CinemachineVirtualCamera CharacterVirtualCam;
    private CinemachineVirtualCamera puzzleVirtualCam;
    private Transform CharacterMesh;

    public void SetCharacterTransform(Transform t)
    {
        CharacterMesh = t;
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
        puzzleVirtualCam.enabled = false;
        puzzleVirtualCam = null;
        CharacterVirtualCam.enabled = true;
        Camera.orthographic = true;
    }
    public void ToggleCamera()
    {
        if (puzzleVirtualCam == null)
            return;
        puzzleVirtualCam.enabled = !puzzleVirtualCam.enabled;
        Camera.orthographic = !Camera.orthographic;
        CharacterVirtualCam.enabled = !CharacterVirtualCam.enabled;
    }
    private void Update()
    {
        if (!Camera.orthographic)
            CharacterMesh.rotation = Quaternion.LookRotation(Camera.transform.forward, Vector3.up);
    }
}
