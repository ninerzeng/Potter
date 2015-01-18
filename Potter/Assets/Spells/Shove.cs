using UnityEngine;
using System.Collections;

public class Shove : SpellBehavior {

	public GameObject character;
	public float impactRadius = 25f;
	public Vector3 force = new Vector3(-3000, 200, 0);

	public override void Attack() {
		Collider[] enemy_colliders = Physics.OverlapSphere(character.transform.position, impactRadius);
		foreach(Collider col in enemy_colliders){
			if (col.gameObject.tag == "Enemy")
				col.attachedRigidbody.AddForce (force);
		}
	}
}