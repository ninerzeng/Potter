using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BoxCollider c = transform.GetComponent<BoxCollider>();
		c.rigidbody.velocity = Vector3.back * 10;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	
	}
}
