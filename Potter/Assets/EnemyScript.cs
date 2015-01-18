using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyScript : MonoBehaviour {
	public float enemyHealth = 100;
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(enemyHealth <= 0)
		{
			GameObject g = GameObject.Find("WormSpawner");
			List<GameObject> l = g.GetComponent<spawner>().enemies;
			l.Remove(this.gameObject);
			Destroy(this);
		}
	}
}
