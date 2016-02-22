using UnityEngine;
using System.Collections;

public class ClipManager : MonoBehaviour {

	private int bulletsLeft = 5;
	private Animator thisAnimator;

	void Start(){
		NotificationCenter.DefaultCenter().AddObserver (this, "StrayBullet");
		thisAnimator = GetComponent<Animator>();
	}

	void StrayBullet(Notification notification){
		bulletsLeft--;
		thisAnimator.SetInteger ("bulletsLeft", bulletsLeft);
		if (bulletsLeft <= 0) NotificationCenter.DefaultCenter().PostNotification(this, "NoBulletsLeft");

	}

}
