using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Library;

public class LiftSpell : MonoBehaviour {

	public GameObject character;
	public float impactRadius = 25f;
	public bool lifted = false;

	bool grounded = false;


	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.LeftControl)){
			Attack();
		} 
	}

	public  void Attack()
	{
		StartCoroutine (liftEnemies());
	}
	
	IEnumerator liftEnemies() {
		GameObject player = GameObject.Find ("First Person Controller");
		print(player.transform.position);
		Collider[] enemy_colliders = Physics.OverlapSphere(player.transform.position, impactRadius);
		
		foreach(Collider col in enemy_colliders) 
		{
			if (col.gameObject.tag == "Enemy") {
				col.rigidbody.velocity = Vector3.up * 2;
				col.rigidbody.useGravity = false;
				lifted = true;
				Seeker seeker = col.GetComponent<Seeker>();
				if (seeker)
				seeker.enabled = false;
				AIPath aipath = col.GetComponent<AIPath>();
				if (aipath)
					aipath.enabled = false;
				//col.GetComponent<EnemyScript>().grounded = false;
				
			}
		}
		
		while(true)
		{
			if(Input.GetKeyUp(KeyCode.LeftControl))
				break;
			yield return null;
		}

		foreach(Collider col in enemy_colliders) 
		{
			if (col.gameObject.tag == "Enemy") {
				col.rigidbody.useGravity = true;
				col.rigidbody.velocity = Vector3.down * 250;
				//col.GetComponent<EnemyScript>().hurt();
				if (col)
					StartCoroutine(bounceDelay(col.gameObject));
			}
		}
	}

	IEnumerator bounceDelay(GameObject wormy) {
		yield return new WaitForSeconds(2.0f);

		Seeker seeker = wormy.GetComponent<Seeker>();
		if (seeker)
			seeker.enabled = true;
		AIPath aipath = wormy.GetComponent<AIPath>();
		if (aipath)
			aipath.enabled = true;
	}
}

	