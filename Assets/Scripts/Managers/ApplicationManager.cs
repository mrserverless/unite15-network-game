using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class ApplicationManager : MonoBehaviour 
{
	void Awake () 
	{
		GameObject.DontDestroyOnLoad( this );
	}	

	void Start () 
	{
		Application.LoadLevel("Lobby Scene");
	}	
}
