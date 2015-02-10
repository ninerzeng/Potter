using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float health = 100;
	GUIBarScript GUIBar;

	// Use this for initialization
	void Start () {
		GUIBar = GetComponentInChildren<GUIBarScript>();
		health= 100;
	}
	
	// Update is called once per frame
	void Update () {
			GUIBar.Value = health / 100f;
		if (health <=0)
			GUIBar.end = true;
	}
}
