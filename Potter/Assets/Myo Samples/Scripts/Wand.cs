using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using Library;

// Change the material when certain poses are made with the Myo armband.
// Vibrate the Myo armband when a fist pose is made.
public class Wand : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;
	public SpellBehavior shoveLeftSpell;
	public SpellBehavior shoveRightSpell;
	public SpellBehavior liftSpell;
	public SpellBehavior fireStorm;
    // Materials to change to when poses are made.
    public Material waveInMaterial;
    public Material waveOutMaterial;
    public Material doubleTapMaterial;
	public Material fistMaterial;
	public Material fingerSpreadMaterial;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

	void Start() {
		StartCoroutine (attackReactor());
	}

    // Update is called once per frame.
    IEnumerator attackReactor ()
    {
		while(true) {
	        // Access the ThalmicMyo component attached to the Myo game object.
	        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

			// Check if the pose has changed since last update.
	        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
	        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
	        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
	        // is not on a user's arm, pose will be set to Pose.Unknown.
	        if (thalmicMyo.pose != _lastPose) {
	            _lastPose = thalmicMyo.pose;

	            // Vibrate the Myo armband when a fist is made.
	            if (thalmicMyo.pose == Pose.Fist) {
					print ("lift");
					thalmicMyo.Vibrate (VibrationType.Short);
					liftSpell.Attack(thalmicMyo);
					renderer.material = fistMaterial;
	                ExtendUnlockAndNotifyUserAction (thalmicMyo);
	            // Change material when wave in, wave out or double tap poses are made.
				} else if (thalmicMyo.pose == Pose.WaveIn) {
					print ("shove left");
					thalmicMyo.Vibrate (VibrationType.Short);

					shoveLeftSpell.Attack (thalmicMyo);
	                renderer.material = waveInMaterial;

	                ExtendUnlockAndNotifyUserAction (thalmicMyo);
	            } else if (thalmicMyo.pose == Pose.WaveOut) {
					print ("shove right");
					thalmicMyo.Vibrate (VibrationType.Short);
					shoveRightSpell.Attack (thalmicMyo);
	                renderer.material = waveOutMaterial;

	                ExtendUnlockAndNotifyUserAction (thalmicMyo);
	            } else if (thalmicMyo.pose == Pose.DoubleTap) {
					thalmicMyo.Vibrate (VibrationType.Short);
	                renderer.material = doubleTapMaterial;
	                ExtendUnlockAndNotifyUserAction (thalmicMyo);
				} else if (thalmicMyo.pose == Pose.FingersSpread && !Accelerometer.forcedGesture(thalmicMyo.accelerometer)) {
					print ("fire!");
					renderer.material = fingerSpreadMaterial;
					thalmicMyo.Vibrate (VibrationType.Short);
					StartCoroutine (denseDamageVibration(thalmicMyo));
					fireStorm.Attack (thalmicMyo);
				}
	        }
			yield return null;
		}
    }

	IEnumerator denseDamageVibration(ThalmicMyo myo) {
		int vibration_count = 0;
		while(vibration_count <= 3) {
			yield return new WaitForSeconds(0.01f);
			myo.Vibrate (VibrationType.Short);
			vibration_count++;
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
