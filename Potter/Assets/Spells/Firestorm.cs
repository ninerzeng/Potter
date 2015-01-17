using UnityEngine;
using System.Collections;


public class Firestorm : SpellBehavior {
	public GameObject nowSpellObj;
	public float spawnRadius;
	void Awake()
	{
		Application.runInBackground = true;
	}
	public override void Attack ()
	{

		for(int i = 0; i <20; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.insideUnitSphere.x * spawnRadius + this.transform.position.x, 
			                                    transform.position.y+ 4.098938f, this.transform.position.z + Random.insideUnitSphere.z * spawnRadius);

			Quaternion myrotate = this.transform.rotation;
			myrotate.eulerAngles = new Vector3(90f, 180f, 0);
	
	
			GameObject fireball = Instantiate (nowSpellObj, spawnPosition, myrotate) as GameObject;
			
			//		Instantiate(nowSpellObj);
			StartCoroutine(TestCoroutine(fireball));
//				TestCoroutine (fireball);
		}
//		Instantiate (nowSpellObj);

//		Destroy(myparticle);
	}
	IEnumerator TestCoroutine(GameObject ToBeDestroyed)
	{
		yield return new WaitForSeconds(3.0f);
		Destroy (ToBeDestroyed);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
		{
			print ("Fire!");
			Attack();
		}
	}
}
