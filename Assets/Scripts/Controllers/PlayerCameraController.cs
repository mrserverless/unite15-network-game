using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCameraController: NetworkBehaviour 
{
	//public variables
	public bool invertAxis = false;	
	public float minPitch = -60.0f;
	public float maxPitch = 60.0f;
	public float pitchSensitivity = 1.0f;
	
	//private variables
	private float pitch = 0.0f;
	[SyncVar] public float syncPitch = 0.0f;
	private Transform playerCamera;
	private float LerpRate = 15.0f;

	void Awake()
	{
		pitch = transform.localEulerAngles.x;
		playerCamera = transform.FindChild("Camera");
	}
	
	void Update()
	{
		if (isLocalPlayer) {
			// get input
			float mouseYInput = Input.GetAxis ("Mouse Y");
			Vector3 newAngles = playerCamera.localEulerAngles;
		
			// calculate pitch
			pitch -= (invertAxis ? -1 : 1) * mouseYInput * pitchSensitivity * Time.deltaTime * 60.0f;
			pitch = Mathf.Clamp (pitch, minPitch, maxPitch);
			CmdUpdatePitch( pitch );
			newAngles.x = pitch;

			// update rotation
			playerCamera.localEulerAngles = newAngles;
		}
		SmoothPitch ();
	}

	void SmoothPitch() 
	{
		if (!isLocalPlayer) {
			Vector3 newAngles = playerCamera.localEulerAngles;
			newAngles.x = Mathf.LerpAngle ( newAngles.x, syncPitch, Time.deltaTime * LerpRate );
			playerCamera.localEulerAngles = newAngles;

		}
	}

	[Command]
	void CmdUpdatePitch ( float pitch ) 
	{
		syncPitch = pitch;
	}
}

