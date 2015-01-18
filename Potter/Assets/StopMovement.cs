using UnityEngine;
using System.Collections;

public class StopMovement : SpellBehavior {
	
	public GameObject character;
	public float impactRadius = 25f;

	public override void Attack(ThalmicMyo myo) {
		Collider[] enemy_colliders = Physics.OverlapSphere(character.transform.position, impactRadius);
		foreach(Collider col in enemy_colliders){
			if (col.gameObject.tag == "Enemy")
				col.rigidbody.velocity = Vector3.zero;
		}
	}
}