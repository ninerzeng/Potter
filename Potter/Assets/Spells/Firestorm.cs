using UnityEngine;
using System.Collections;


public class Firestorm : SpellBehavior {
	public GameObject nowSpellObj;
	public float spawnRadius = 25f;
	public int numFireBalls = 20;
	public float impactRadius = 25f;
	public override void Attack (ThalmicMyo myo)
	{
		for(int i = 0; i < numFireBalls; i++)
		{
			StartCoroutine (spitFireBall());
		}
	}

	IEnumerator spitFireBall()
	{
		Vector3 spawnPosition = new Vector3(Random.insideUnitSphere.x * spawnRadius + this.transform.position.x, 
		                                    transform.position.y+ 4.098938f, this.transform.position.z + Random.insideUnitSphere.z * spawnRadius);

		Collider[] enemy_colliders = Physics.OverlapSphere(spawnPosition, impactRadius);
		Quaternion myrotate = this.transform.rotation;
		myrotate.eulerAngles = new Vector3(90f, 180f, 0);

		GameObject fireball = Instantiate (nowSpellObj, spawnPosition, myrotate) as GameObject;
		yield return new WaitForSeconds(3.0f);

		foreach(Collider col in enemy_colliders) 
		{
			if (col.gameObject.tag == "Enemy") {
				col.gameObject.GetComponent<EnemyScript>().enemyHealth -= 40;
			}
		}
		Destroy (fireball);
	}
}
