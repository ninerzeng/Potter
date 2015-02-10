using UnityEngine;
using System.Collections;

public class EnemyScreept : MonoBehaviour {
	Transform target;
	float moveSpeed = 3f;
	float rotationSpeed = 3f;
	float range = 10f;
	float range2 = 10f;
	float stop = 0f;
	public float health = 100f;
	Transform myTransform;
	// Use this for initialization
	void Awake () {
		myTransform = transform; //cache transform data for easy access/preformance
		
	}

	public void hurt(){
		health -= 1f;
	}
	
	void Start(){
		target = GameObject.FindWithTag("Player").transform; //target the player
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(myTransform.position, target.position);
		if (distance<=range2 &&  distance>=range){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
		}
		
		else if(distance<=range && distance>stop){
			
			//move towards the player
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
		else if (distance<=stop) {
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
		}
		
	}

	void FixedUpdate(){
		if(health <= 0)
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "fireShot"){
			health -= 1f;
		} 
	}

}
