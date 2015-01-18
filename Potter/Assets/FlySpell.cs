using UnityEngine;
using System.Collections;

public class FlySpell : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Input.GetKey(KeyCode.F))
		{
			print ("HI");
			transform.GetComponent<OVRPlayerController>().JumpForce = 20.0f;
			transform.GetComponent<OVRPlayerController>().GravityModifier = 0f;
		}

		if(Input.GetKey(KeyCode.D))
		{
			transform.GetComponent<OVRPlayerController>().JumpForce = 2f;
			transform.GetComponent<OVRPlayerController>().GravityModifier = 1.0f;
		}
	}
}
