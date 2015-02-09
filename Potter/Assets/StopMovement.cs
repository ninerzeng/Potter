using UnityEngine;
using System.Collections;

public class StopMovement : MonoBehaviour {
	
	public GameObject character;
	public float impactRadius = 25f;

	public void Attack() {
		Collider[] enemy_colliders = Physics.OverlapSphere(character.transform.position, impactRadius);
		foreach(Collider col in enemy_colliders){
			if (col.gameObject.tag == "Enemy")
				col.rigidbody.velocity = Vector3.zero;
		}
	}
}