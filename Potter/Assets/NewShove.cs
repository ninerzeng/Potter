using UnityEngine;
using System.Collections;

public class NewShove : SpellBehavior {
	
	public GameObject character;
	public float impactRadius = 25f;
	//0 is left, 1 is right
	public int left_or_right=0;
	private Vector3 force = new Vector3(0, 200, 0);
	
	public override void Attack(ThalmicMyo myo) {
		Collider[] enemy_colliders = Physics.OverlapSphere(character.transform.position, impactRadius);
		Vector3 angles = character.transform.eulerAngles;
		//right
		if (left_or_right == 1) {
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
		else if (left_or_right == 0) {
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
			if (col.gameObject.tag == "Enemy")
				col.attachedRigidbody.AddForce (force);
		}
	}
}