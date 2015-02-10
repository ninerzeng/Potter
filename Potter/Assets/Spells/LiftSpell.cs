using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Library;

public class LiftSpell : MonoBehaviour {

	public GameObject character;
	public float impactRadius = 25f;
	public bool lifted = false;
	Vector3 angles;
	private Vector3 force = new Vector3(0, 50, 0);
	
	bool grounded = false;


	void FixedUpdate(){
		GameObject player = GameObject.Find ("First Person Controller");
		
		angles = player.transform.eulerAngles;
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
		Collider[] enemy_colliders = Physics.OverlapSphere(player.transform.position, 10f);
		
		foreach(Collider col in enemy_colliders) 
		{
			if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Block") {
				if(col && col.gameObject.tag == "Enemy"){
					col.gameObject.GetComponent<EnemyScreept>().lifted = true;
				}
				col.rigidbody.velocity = Vector3.up * 3;
				col.rigidbody.useGravity = false;
				lifted = true;
				Seeker seeker = col.GetComponent<Seeker>();
				if (seeker)
				seeker.enabled = false;				
			}
		}
		
		while(true)
		{
			if(Input.GetKeyUp(KeyCode.LeftControl))
				break;
			if(Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K)){
				moveBlocks(enemy_colliders);
			}
			yield return null;
		}

		foreach(Collider col in enemy_colliders) 
		{
			if (col.gameObject.tag == "Enemy") {
				col.gameObject.GetComponent<EnemyScreept>().lifted = false;
				col.rigidbody.useGravity = true;
				col.rigidbody.velocity = Vector3.down * 500;
				//col.GetComponent<EnemyScript>().hurt();
			} else if(col.gameObject.tag == "Block"){
				col.rigidbody.velocity = Vector3.zero;
			}
				
		}
	}

	void moveBlocks(Collider[] enemy_colliders){
		if (Input.GetKey(KeyCode.K)) {
			print ("right");
			if (angles.y >= 315 || angles.y < 45) {
				force.x = 1000;
				print ("N");
			} 
			else if (angles.y >= 45 && angles.y < 135) {
				force.x = 0;
				force.z = -1000;
				print("E");
			} 
			else if (angles.y >= 135 && angles.y < 225) {
				force.x = -1000;
				print ("S");
			} 
			else {
				force.x = 0;
				force.z = 1000;
				print ("W");
			} 
		}
		//left
		else if(Input.GetKeyDown(KeyCode.J)){
			print ("left");
			if (angles.y >= 315 || angles.y < 45) {
				force.x = -1000;
				print ("N");
			} else if (angles.y >= 45 && angles.y < 135) {
				force.x = 0;
				force.z = 1000;
				print ("E");
			} else if (angles.y >= 135 && angles.y < 225) {
				force.x = 1000;
				print ("S");
			} else {
				force.x = 0;
				force.z = -1000;
				print ("W");
			}
		}
		foreach(Collider col in enemy_colliders){
			if (col.gameObject.tag == "Block"){
				col.rigidbody.velocity = Vector3.zero;
				col.attachedRigidbody.AddForce (force);
			}
		}
	}


}

	