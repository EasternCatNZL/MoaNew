using UnityEngine;
using System.Collections;

public class ThirdPersonOrbitCam : MonoBehaviour 
{
    //Object to track ref
    [Header("Player Object")]
	public Transform player;                                           // Player's reference.

    //Camera control vars
    [Header("Camera Control vars")]
	public Vector3 pivotOffset = new Vector3(0.0f, 1.0f,  0.0f);       // Offset to repoint the camera.
	public Vector3 camOffset   = new Vector3(0.0f, 0.7f, -3.0f);       // Offset to relocate the camera related to the player position.
	public float smooth = 10f;                                         // Speed of camera responsiveness.
	public float horizontalAimingSpeed = 400f;                         // Horizontal turn speed.
	public float verticalAimingSpeed = 400f;                           // Vertical turn speed.
	public float maxVerticalAngle = 30f;                               // Camera max vertical clamp angle. 
	public float minVerticalAngle = -60f;                              // Camera min vertical clamp angle.
    //public float maxHorizontalAngle = 50f;

	private float angleH = 0;                                          // Float to store camera horizontal angle related to mouse movement.
	private float angleV = 0;                                          // Float to store camera vertical angle related to mouse movement.
	private Vector3 relCameraPos;                                      // Current camera position relative to the player.
	private float relCameraPosMag;                                     // Current camera distance to the player.
	private Vector3 smoothPivotOffset;                                 // Camera current pivot offset on interpolation.
	private Vector3 smoothCamOffset;                                   // Camera current offset on interpolation.
	private Vector3 targetPivotOffset;                                 // Camera pivot offset target to iterpolate.
	private Vector3 targetCamOffset;                                   // Camera offset target to interpolate.
	private float defaultFOV;                                          // Default camera Field of View.
	private float targetFOV;                                           // Target camera FIeld of View.
	private float targetMaxVerticalAngle;                              // Custom camera max vertical clamp angle. 

    //camera transform ref
    private Transform cam;                                             // This transform.

    void Awake()
	{
		// Reference to the camera transform.
		cam = transform;

		// Set camera default position.
		cam.position = player.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
		cam.rotation = Quaternion.identity;
        
		// Get camera position relative to the player, used for collision test.
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;

		// Set up references and default values.
		smoothPivotOffset = pivotOffset;
		smoothCamOffset = camOffset;
		defaultFOV = cam.GetComponent<Camera>().fieldOfView;

		ResetTargetOffsets ();
		ResetFOV ();
		ResetMaxVerticalAngle();
	}

	void LateUpdate()
    {
        //get input for camera movement
        MovementInput();
        //clamp the angles
        ClampAngles();
        // Set camera orientation..
        Quaternion camYRotation, aimRotation;
        SetRotation(out camYRotation, out aimRotation);

        // Set FOV.
        SetFOV();

        // Test for collision with the environment based on current camera position.
        Vector3 noCollisionOffset = CameraCollisionCheck(camYRotation, aimRotation);

        // Repostition the camera.
        RepositionCamera(camYRotation, aimRotation, noCollisionOffset);
    }

    //Repositions the camera
    private void RepositionCamera(Quaternion camYRotation, Quaternion aimRotation, Vector3 noCollisionOffset)
    {
        smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
        smoothCamOffset = Vector3.Lerp(smoothCamOffset, noCollisionOffset, smooth * Time.deltaTime);

        cam.position = player.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;
    }

    //checks if camera is currently colliding with terrain
    private Vector3 CameraCollisionCheck(Quaternion camYRotation, Quaternion aimRotation)
    {
        Vector3 baseTempPosition = player.position + camYRotation * targetPivotOffset;
        Vector3 noCollisionOffset = targetCamOffset;
        for (float zOffset = targetCamOffset.z; zOffset <= 0; zOffset += 0.5f)
        {
            noCollisionOffset.z = zOffset;
            if (DoubleViewingPosCheck(baseTempPosition + aimRotation * noCollisionOffset, Mathf.Abs(zOffset)) || zOffset == 0)
            {
                break;
            }
        }

        return noCollisionOffset;
    }

    //sets the fov of camera
    private void SetFOV()
    {
        cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, targetFOV, Time.deltaTime);
    }

    //sets camera rotation
    private void SetRotation(out Quaternion camYRotation, out Quaternion aimRotation)
    {
        camYRotation = Quaternion.Euler(0, angleH, 0);
        aimRotation = Quaternion.Euler(-angleV, angleH, 0);
        cam.rotation = aimRotation;
    }

    private void ClampAngles()
    {
        // Set vertical movement limit.
        angleV = Mathf.Clamp(angleV, minVerticalAngle, targetMaxVerticalAngle);
        //angleH = Mathf.Clamp(angleH, player.transform.rotation.eulerAngles.y - maxHorizontalAngle, player.transform.rotation.eulerAngles.y + maxHorizontalAngle);
    }

    private void MovementInput()
    {
        // Get mouse movement to orbit the camera.
        angleH += Mathf.Clamp(Input.GetAxis("Joy X"), -1, 1) * horizontalAimingSpeed * Time.deltaTime;
        angleV += Mathf.Clamp(Input.GetAxis("Joy Y"), -1, 1) * verticalAimingSpeed * Time.deltaTime;
    }

    // Set camera offsets to custom values.
    public void SetTargetOffsets(Vector3 newPivotOffset, Vector3 newCamOffset)
	{
		targetPivotOffset = newPivotOffset;
		targetCamOffset = newCamOffset;
	}

	// Reset camera offsets to default values.
	public void ResetTargetOffsets()
	{
		targetPivotOffset = pivotOffset;
		targetCamOffset = camOffset;
	}

	// Set camera vertical offset.
	public void SetYCamOffset(float y)
	{
		targetCamOffset.y = y;
	}

	// Set custom Field of View.
	public void SetFOV(float customFOV)
	{
		this.targetFOV = customFOV;
	}

	// Reset Field of View to default value.
	public void ResetFOV()
	{
		this.targetFOV = defaultFOV;
	}

	// Set max vertical camera rotation angle.
	public void SetMaxVerticalAngle(float angle)
	{
		this.targetMaxVerticalAngle = angle;
	}

	// Reset max vertical camera rotation angle to default value.
	public void ResetMaxVerticalAngle()
	{
		this.targetMaxVerticalAngle = maxVerticalAngle;
	}

	// Double check for collisions: concave objects doesn't detect hit from outside, so cast in both directions.
	bool DoubleViewingPosCheck(Vector3 checkPos, float offset)
	{
		float playerFocusHeight = player.GetComponent<CapsuleCollider> ().height *0.5f;
		return ViewingPosCheck (checkPos, playerFocusHeight) && ReverseViewingPosCheck (checkPos, playerFocusHeight, offset);
	}

	// Check for collision from camera to player.
	bool ViewingPosCheck (Vector3 checkPos, float deltaPlayerHeight)
	{
		RaycastHit hit;
		
		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, player.position+(Vector3.up* deltaPlayerHeight) - checkPos, out hit, relCameraPosMag))
		{
			// ... if it is not the player...
			if(hit.transform != player && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				// This position isn't appropriate.
				return false;
			}
		}
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		return true;
	}

	// Check for collision from player to camera.
	bool ReverseViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight, float maxDistance)
	{
		RaycastHit hit;

		if(Physics.Raycast(player.position+(Vector3.up* deltaPlayerHeight), checkPos - player.position, out hit, maxDistance))
		{
			if(hit.transform != player && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				return false;
			}
		}
		return true;
	}

	// Get camera magnitude.
	public float getCurrentPivotMagnitude(Vector3 finalPivotOffset)
	{
		return Mathf.Abs ((finalPivotOffset - smoothPivotOffset).magnitude);
	}
}
