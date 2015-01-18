using UnityEngine;
using System.Collections;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class Side : MonoBehaviour {
	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;
	private GameObject[] enemy_array;
	private Collider[] enemy_array_c;
	// Use this for initialization

	void Update () {
	ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
	if (thalmicMyo.pose != _lastPose) {
		Vector3 pos = transform.position; 
		_lastPose = thalmicMyo.pose;

		enemy_array_c = Physics.OverlapSphere(transform.position, 25.0f);
		
		// Vibrate the Myo armband when a fist is made.
		if (thalmicMyo.pose == Pose.Fist) {
			thalmicMyo.Vibrate (VibrationType.Medium);
			//reset location
			//				Vector3 still = (0);
			pos.x = 0;
			pos.y = 0;
			pos.z = 0;
			//transform.position = pos;
			foreach(Collider col in enemy_array_c){
					rigidbody.velocity = pos;;
			}
			rigidbody.velocity = pos;
			ExtendUnlockAndNotifyUserAction (thalmicMyo);
			
			// Move the object to the left
		} else if (thalmicMyo.pose == Pose.WaveIn) {
			//				renderer.material = waveInMaterial;
			
			print ("waved left");
			//pos.x -= 10f;
			//transform.position = pos;
			//rigidbody.AddForce (Vector3.up * 10);
			foreach(Collider col in enemy_array_c){
					col.attachedRigidbody.AddForce (-3000, 200, 0);
				}
			//rigidbody.AddForce (-3000, 200, 0);
			//transform.Translate (Vector3.right * Time.deltaTime, OVRCameraRig.transform);
			ExtendUnlockAndNotifyUserAction (thalmicMyo);
			
			//move the object to the right
		} else if (thalmicMyo.pose == Pose.WaveOut) {
			//				renderer.material = waveOutMaterial;

			print ("waved right");
				foreach(Collider col in enemy_array_c){
					col.attachedRigidbody.AddForce (3000, 200, 0);
				}
			//pos.x += 10f;
			//transform.position = pos;
			ExtendUnlockAndNotifyUserAction (thalmicMyo);
			
		} //else if (thalmicMyo.pose == Pose.DoubleTap) {
		//				renderer.material = doubleTapMaterial;
		//				
		//				ExtendUnlockAndNotifyUserAction (thalmicMyo);
		//			}
	}
}

// Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
// recognized.
void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
{
	ThalmicHub hub = ThalmicHub.instance;
	
	if (hub.lockingPolicy == LockingPolicy.Standard) {
		myo.Unlock (UnlockType.Timed);
	}
	
	myo.NotifyUserAction ();
}
}