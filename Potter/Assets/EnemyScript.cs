using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyScript : MonoBehaviour {
	public float enemyHealth = 100;
	bool dead = false;
	public bool grounded = false;
	
	// Update is called once per frame
	void FixedUpdate () {
		if(enemyHealth <= 0 && !dead && grounded)
		{
			dead = true;
			StartCoroutine (delayedDeath ());
		}
	}

	public void hurt() {
		enemyHealth -= 10f;
	}

	IEnumerator delayedDeath() {
		gameObject.SetActive(false);
		yield return new WaitForSeconds(3.0f);
		GameObject g = GameObject.Find("WormSpawner");
		List<GameObject> l = g.GetComponent<spawner>().enemies;
		l.Remove(gameObject);
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Terrain")
			grounded = true;
	}
}
