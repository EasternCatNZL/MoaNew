using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCinematics : MonoBehaviour
{

    public enum CameraModes
    {
        NONE = 0,
        SPIRALZOOM = 1
    };

    [Header("Camera Settings")]
    public CameraModes Mode = CameraModes.NONE;
    public GameObject CameraObject = null;
    public Vector3 CameraTarget = Vector3.zero;
    [Header("Movement Settings")]
    public float Duration = 0.0f;
    public Vector3 StartPosition = Vector3.zero;
    public Vector3 EndPosition = Vector3.zero;
    [Header("Zoom Settings")]
    public float ZoomStart = 4.0f;
    public float ZoomEnd = 1.0f;
    private float ZoomAmount;
    [Header("Screen Fade Settings")]
    public GameObject FadingImage;
    [Header("Scene Management")]
    public int SceneNum = 0;
    [Header("Lighting")]
    public GameObject Mainlight;

    private float heightDifference;
    private bool running = false;
    private float LerpTime;
    public float Timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        CameraObject = GameObject.FindGameObjectWithTag("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            switch (Mode)
            {
                case CameraModes.SPIRALZOOM:
                    if (StartPosition == Vector3.zero)
                    {
                        StartPosition = new Vector3(0.0f, 0.0f, 2.0f);
                    }
                    if (EndPosition == Vector3.zero)
                    {
                        EndPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    }
                    SpiralZoom();
                    break;
                default: break;
            }
            if (Time.time - Timer > Duration)
            {
                running = false;
                Timer = 0.0f;
                switch (SceneNum)
                {
                    case 0:
                        CameraObject.GetComponent<ThirdPersonOrbitCam>().pivotOffset = new Vector3(0.0f, 1.0f, 1.0f);
                        CameraObject.GetComponent<ThirdPersonOrbitCam>().camOffset = new Vector3(0.0f, 1.0f, -2.0f);
                        CameraObject.GetComponent<ThirdPersonOrbitCam>().player = GameObject.Find("Moa_Chick_Walking").transform;
                        CameraObject.GetComponent<ThirdPersonOrbitCam>().enabled = true;
                        CameraObject.GetComponent<ThirdPersonOrbitCam>().Init();
                        GameObject.FindGameObjectWithTag("Chick").GetComponent<MoveBehaviour>().disabled = false;
                        break;
                    case 1:
                        SceneManager.LoadScene(0);
                        break;
                    default: break;
                }
            }
        }
    }

    void SpiralZoom()
    {
        //Rotate the camera around the target
        float RotateAmount = (Vector3.Angle(StartPosition, EndPosition) / Duration);
        transform.Rotate(new Vector3(0.0f, RotateAmount * Time.deltaTime, 0.0f));

        //Change camera height and zoom

        CameraObject.transform.localPosition = CameraObject.transform.localPosition + new Vector3(0.0f, heightDifference * Time.deltaTime, 0.0f);
        CameraObject.transform.localPosition = CameraObject.transform.localPosition - new Vector3(0.0f, 0.0f, ZoomAmount * Time.deltaTime);

        //Rotate Camera to look at target
        CameraObject.transform.right = Vector3.Cross(CameraObject.transform.up, CameraTarget - CameraObject.transform.position);
        CameraObject.transform.forward = CameraTarget - CameraObject.transform.position;
    }

    public void runCamera()
    {
        running = true;
        switch (SceneNum)
        {
            case 0:
                Mainlight.transform.rotation = Quaternion.Euler(new Vector3(-15, -30, 0.0f));
                Mainlight.GetComponent<Light>().intensity = 0.35f;
                CameraObject.GetComponent<ThirdPersonOrbitCam>().enabled = false;
                CameraObject.transform.position = transform.position + new Vector3(0.0f, 0.5f, ZoomStart);
                heightDifference = (EndPosition.y - StartPosition.y) / Duration;
                ZoomAmount = (ZoomStart - ZoomEnd) / Duration;
                transform.position = CameraTarget;
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 154.0f, 0.0f));
                break;
            default: break;
        }
        Timer = Time.time;
    }

}
