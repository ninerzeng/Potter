using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pose = Thalmic.Myo.Pose;
using Library;

public class LiftSpell : SpellBehavior {

	public GameObject character;
	public float impactRadius = 25f;

	Pose lastPose = Pose.Unknown;
	bool grounded = false;

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
				Seeker seeker = col.GetComponent<Seeker>();
				if (seeker)
					seeker.enabled = false;
				AIPath aipath = col.GetComponent<AIPath>();
				if (aipath)
					aipath.enabled = false;
				col.GetComponent<EnemyScript>().grounded = false;
				
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
				col.GetComponent<EnemyScript>().hurt();
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

	