using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pose = Thalmic.Myo.Pose;
using Library;

public class LiftSpell : SpellBehavior {

	public GameObject character;
	public float impactRadius = 25f;

	Pose lastPose = Pose.Unknown;

	public override void Attack(ThalmicMyo myo)
	{
		StartCoroutine (liftEnemies(myo));
	}

	IEnumerator liftEnemies(ThalmicMyo myo) {
		Collider[] enemy_colliders = Physics.OverlapSphere(character.transform.position, impactRadius);
		
		foreach(Collider col in enemy_colliders) 
		{
			if (col.gameObject.tag == "Enemy") {
				col.rigidbody.velocity = Vector3.up * 2;
				col.rigidbody.useGravity = false;
			}
		}
		
		while(true)
		{
			if(lastPose != myo.pose && myo.pose == Pose.FingersSpread && Accelerometer.forcedGesture(myo.accelerometer))
				break;
			yield return null;
		}

		foreach(Collider col in enemy_colliders) 
		{
			if (col.gameObject.tag == "Enemy") {
				col.rigidbody.useGravity = true;
				col.rigidbody.velocity = Vector3.down * 250;
			}
		}
	}
}

	