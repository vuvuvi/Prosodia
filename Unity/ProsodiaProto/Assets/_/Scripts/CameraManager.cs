using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera Camera;
    public CinemachineVirtualCamera CharacterVirtualCam;
    private void OnTriggerEnter(Collider other)
    {
        var virtualCam = other.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        if (virtualCam == null)
            return;
        virtualCam.enabled = true;
        Camera.orthographic = false;
        CharacterVirtualCam.enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        var virtualCam = other.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        if (virtualCam == null)
            return;
        virtualCam.enabled = false;
        CharacterVirtualCam.enabled = true;
        Camera.orthographic = true;
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
