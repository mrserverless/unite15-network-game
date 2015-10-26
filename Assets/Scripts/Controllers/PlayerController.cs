using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]
public class PlayerController: NetworkBehaviour 
{
	//public variables
	public bool invertAxis = false;
	public float gravity = -9.81f;
	public float speed = 5.0f;
	public float strafeSpeed = 5.0f;
	public float yawSensitivity = 1.0f;
	
	//private variables
	private float yaw = 0.0f;
	private CharacterController characterController;
	private Transform playerCamera;
	
	void Awake()
	{
		yaw = transform.localEulerAngles.y;
		
		// get a reference to the Character Controller
		characterController = GetComponent<CharacterController>();
	}

	void Start()
	{
		if (!isLocalPlayer) {
			Transform playerCameraTransform = transform.FindChild("Camera");
			Camera playerCamera = playerCameraTransform.GetComponent<Camera>();
			AudioListener playerAudioListener = playerCameraTransform.GetComponent<AudioListener>();
			if (playerCamera)
				playerCamera.enabled = false;
			if (playerAudioListener)
				playerAudioListener.enabled = false;
		}
	}
	
	void Update()
	{
		if (isLocalPlayer) {
			// get input
			float verticalInput = Input.GetAxisRaw ("Vertical") * Time.deltaTime;
			float horizontalInput = Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
			float mouseXInput = Input.GetAxis ("Mouse X");
		
			Vector3 newAngles = transform.localEulerAngles;
		
			// calculate yaw
			yaw -= (invertAxis ? -1 : 1) * mouseXInput * yawSensitivity * Time.deltaTime * 60.0f;
			newAngles.y = yaw;
		
			// update rotation
			transform.localEulerAngles = newAngles;
		
			// move character controller
			characterController.Move (transform.forward * verticalInput * speed);
			characterController.Move (transform.right * horizontalInput * strafeSpeed);
		
			//gravity
			characterController.Move (Vector3.up * gravity * Time.deltaTime);
		}
	}
}

