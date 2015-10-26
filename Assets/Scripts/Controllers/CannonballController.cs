using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CannonballController : NetworkBehaviour
{
	private float age;
	private float maxAge = 10.0f;

	// Use this for initialization
	void Start ()
	{
		age = 0.0f;	
	}
	
	// Update is called once per frame
	[ServerCallback]
	void Update () 
	{	
		age += Time.deltaTime;
		if( age > maxAge )
		{	
			NetworkServer.Destroy(gameObject);
		}
	}
}
