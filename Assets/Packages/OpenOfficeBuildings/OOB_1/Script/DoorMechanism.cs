using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour {

	public GameObject Door;

	public Vector3 doorPosOpen, doorPosClose;

	public float rotSpeed = 1f;

	public bool doorMechBool;


	void Start () 
	{
		
	}
	

	void Update () 
	{
		if (doorMechBool)
			Door.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (doorPosOpen), rotSpeed * Time.deltaTime);
		else 
			Door.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (doorPosClose), rotSpeed * Time.deltaTime);
	}
}
