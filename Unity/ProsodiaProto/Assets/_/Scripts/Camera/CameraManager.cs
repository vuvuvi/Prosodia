using Cinemachine;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera Camera;
    public CinemachineVirtualCamera CharacterVirtualCam;
    private CinemachineVirtualCamera puzzleVirtualCam;
    private Transform CharacterMesh;
    private PerspectiveSwitcher perspectiveSwitcher;
    private bool isOrtho;

    private void Start()
    {
        var allVirtualCamera = FindObjectsOfType<CinemachineVirtualCamera>().ToList();
        allVirtualCamera.ForEach(vc => vc.enabled = false);
        perspectiveSwitcher = Camera.gameObject.GetComponent<PerspectiveSwitcher>();
        ResetCamera();
    }

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
        ResetCamera();
    }

    private void ResetCamera()
    {
        if (puzzleVirtualCam != null) puzzleVirtualCam.enabled = false;
        puzzleVirtualCam = null;
        CharacterVirtualCam.enabled = true;
        perspectiveSwitcher.SetOrthographic();
        isOrtho = true;
    }

    public void ToggleCamera()
    {
        if (puzzleVirtualCam == null)
            return;
        isOrtho = !isOrtho;
        puzzleVirtualCam.enabled = !puzzleVirtualCam.enabled;
        perspectiveSwitcher.ToggleCamera();
        CharacterVirtualCam.enabled = !CharacterVirtualCam.enabled;

        if(isOrtho)
            CharacterMesh.localRotation = Quaternion.Euler(0,0,0);
    }
    private void Update()
    {
        if(!isOrtho)
            CharacterMesh.rotation = Quaternion.LookRotation(Camera.transform.forward, Vector3.up);
    }
}
