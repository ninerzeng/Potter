using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class spawner : MonoBehaviour {

	float spawnRadius = 30.0f;
	public List<GameObject> enemies;
	public GameObject enemy;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(enemies.Count < 100)
		{
			Vector3 spawnPosition = new Vector3(Random.insideUnitSphere.x * spawnRadius + this.transform.position.x, 
			                                    transform.position.y+ 4.098938f, this.transform.position.z + Random.insideUnitSphere.z * spawnRadius);

			GameObject en = Instantiate(enemy, spawnPosition, Quaternion.identity) as GameObject; 
			enemies.Add (en);
		}
	}
}
