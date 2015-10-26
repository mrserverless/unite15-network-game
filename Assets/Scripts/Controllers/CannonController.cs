using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CannonController : NetworkBehaviour
{
	private float power;
	private Transform playerCamera;

	// Use this for initialization
	void Start ()
	{
		power = 800.0f;
		playerCamera = transform.FindChild("Camera");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isLocalPlayer) {
			if (Input.GetKeyDown (KeyCode.Space) == true) {
				CmdSpawnCannonball ();
			}
		}
	}

	[Command]
	void CmdSpawnCannonball ()
	{
		//we instantiate one from Resources
		GameObject instance = Instantiate (Resources.Load ("Ball")) as GameObject;
		//Let's name it
		instance.name = "Cannonball";
		//Let's position it at the player
		instance.transform.position = playerCamera.position + playerCamera.forward * 1.5f + playerCamera.up * -.5f;
		instance.GetComponent<Rigidbody> ().AddForce (playerCamera.forward * power);
		NetworkServer.Spawn (instance);
	}
}
