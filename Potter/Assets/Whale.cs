using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.velocity = Vector3.back * 10;
	}

	void Update() {
		if (transform.position.z <= -1000f) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 1000f);
		}
	}
}
