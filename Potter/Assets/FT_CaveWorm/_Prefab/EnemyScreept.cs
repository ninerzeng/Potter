using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScreept : MonoBehaviour {
	Transform target;
	public bool lifted = false;
	float moveSpeed = 9f;
	float rotationSpeed = 6f;
	float range = 1000f;
	float range2 = 1000f;
	float stop = 0f;
	public float health = 1000f;
	public static int deaths = 0;
	private int score_int = 0;
	public float DoT=3;
	public Text score;
	Transform myTransform;
	public float attackRange=3;
	// Use this for initialization
	void Awake () {
		myTransform = transform; //cache transform data for easy access/preformance
		GameObject scoreGO = GameObject.Find("Current Score");
		score = scoreGO.GetComponent<Text>();
		score.text = "Current Score: " + "0";
	}

	public void hurt(){
		health -= 1f;
	}
	
	void Start(){
		target = GameObject.FindWithTag("Player").transform; //target the player
	}
	// Update is called once per frame
	void Update () {
		if(lifted) return;
		if( Vector3.Distance(target.transform.position, this.transform.position ) < attackRange) {
			PlayerHealth hp = target.GetComponent<PlayerHealth>();
			hp.health -= Time.fixedDeltaTime * DoT;
		}

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
			deaths ++;
			Destroy (this.gameObject);

			score_int = deaths * 100;
			score.text = "Current Score: " + score_int.ToString ();
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "fireShot"){
			health -= 1f;
		} 
	}

}
