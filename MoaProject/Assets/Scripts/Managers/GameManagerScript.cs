using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public GameObject MotherCall = null;
    public GameObject MotherMoa = null;
    public GameObject BabyMoa = null;
    public GameObject BabyWalkingMoa = null;
    public GameObject WeatherSystem = null;
    public CameraCinematics CinCam = null;
    public GameObject BlackoutImage = null;
    [Header("Time Variables")]
    public float timeLimit = 300.0f; //time limit of game
    public float MotherMoaDeadCountdown = 120.0f;

    private bool StartDeathScene = false;
    private bool DoneDeathScene = false;
    private bool EndGame = false;
    private float EndGameCooldown = 0.0f;

	// Use this for initialization
	void Start () {
        //set up time limit from when scene starts
        timeLimit += Time.time;
        MotherMoaDeadCountdown += Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckTimeLimit();
        if(StartDeathScene && !DoneDeathScene)
        {
            SetupDeathScene();
        }
    }

    private void CheckTimeLimit()
    {
        //if time greater than time limit
        if (Time.time >= MotherMoaDeadCountdown - 4  && !DoneDeathScene)
        {
            BlackoutImage.GetComponent<FadeImage>().StartFadeIn();
            //do game over stuff
        }
        if (Time.time >= MotherMoaDeadCountdown && !DoneDeathScene)
        {
            StartDeathScene = true;
            //do game over stuff
        }
    }

    private void SetupDeathScene()
    {
        //Setup Cinematic Camera
        CinCam.Mode = CameraCinematics.CameraModes.SPIRALZOOM;
        CinCam.CameraTarget = GameObject.FindGameObjectWithTag("ChickCamera").transform.position;
        print(GameObject.FindGameObjectWithTag("ChickCamera").transform.position.ToString());
        //CinCam.transform.position = CinCam.CameraTarget.transform.position;
        //CinCam.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 154.0f, 0.0f));
        GameObject.FindGameObjectWithTag("Chick").GetComponent<Animator>().SetTrigger("Standup");
        CinCam.Duration = 5.0f;
        CinCam.StartPosition = new Vector3(0.0f, 0.0f, 2.0f);
        CinCam.EndPosition = new Vector3(0.0f, -0.5f, -2.0f);
        CinCam.ZoomStart = 4.0f;
        CinCam.ZoomEnd = 1.0f;
        //Run Cinematic Camera
        CinCam.runCamera();

        MotherCall.GetComponent<LoopAudio>().Loop = true;
        MotherMoa.transform.position = new Vector3(0.0f, -20.0f, 0.0f);
        MotherMoa.SetActive(false);
        BabyMoa.SetActive(false);
        BabyWalkingMoa.SetActive(true);
        BabyWalkingMoa.GetComponent<MoveBehaviour>().disabled = true;
        BabyWalkingMoa.GetComponent<Animator>().SetTrigger("Standup");
        WeatherSystem.GetComponent<DynamicWeather>()._Player = BabyWalkingMoa.transform;

        DoneDeathScene = true;
    }

    public void EndScene()
    {
        //Setup Cinematic Camera
        CinCam.SceneNum = 1;
        CinCam.Mode = CameraCinematics.CameraModes.SPIRALZOOM;
        CinCam.CameraTarget = GameObject.Find("CameraFocus_2").transform.position;
        //CinCam.transform.position = CinCam.CameraTarget.transform.position;
        //CinCam.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 154.0f, 0.0f));
        CinCam.Duration = 5.0f;
        CinCam.StartPosition = new Vector3(0.0f, 0.0f, 2.0f);
        CinCam.EndPosition = new Vector3(0.0f, 2.0f, -2.0f);
        CinCam.ZoomStart = 2.0f;
        CinCam.ZoomEnd = 8.0f;
        //Run Cinematic Camera
        CinCam.runCamera();

        EndGame = true;
        EndGameCooldown = Time.time;
    }
}
