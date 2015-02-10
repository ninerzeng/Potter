using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			Application.LoadLevel("Demo_Scene");
		}
	}
}
