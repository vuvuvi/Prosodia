using UnityEngine;

[RequireComponent(typeof(MatrixBlender))]
public class PerspectiveSwitcher : MonoBehaviour
{
    private Matrix4x4 ortho,
                        perspective;
    public float fov = 40f,
                 near = .3f,
                 far = 1000f,
                 orthographicSize = 10f,
                 duration = 1f;
    private float aspect;
    private MatrixBlender blender;
    private bool orthoOn;
    private new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        camera.projectionMatrix = ortho;
        orthoOn = true;
        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));
    }

    void Update()
    {
        //TestCameraToggle();
    }

    private void TestCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            orthoOn = !orthoOn;
            ChangeCameraProjectionMatrix();
        }
    }

    private void ChangeCameraProjectionMatrix()
    {
        if (orthoOn)
            blender.BlendToMatrix(ortho, duration);
        else
            blender.BlendToMatrix(perspective, duration);
    }

    public void ToggleCamera()
    {
        orthoOn = !orthoOn;
        ChangeCameraProjectionMatrix();
    }

    internal void SetOrthographic()
    {
        orthoOn = true;
    }
}