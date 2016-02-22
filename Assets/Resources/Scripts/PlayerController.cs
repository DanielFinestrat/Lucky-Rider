using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	bool firstTime = true;

	private Animator anim;
	private PlayerController thisScript;
	public Weapon weapon;

	public float fireRate = 0f;
	private float timeToFire;

	private bool x = false;
	private bool y = false;
	private bool shootingStarted = false;
	private bool playerIsDead = false;

	private Vector2 centerScreen = new Vector2 (Screen.width/2, Screen.height/2);

	void Start (){
		NotificationCenter.DefaultCenter ().AddObserver (this, "PlayerIsDead");
		anim = GetComponent<Animator>();
		thisScript = GetComponent<PlayerController>();
	}

	void Update () {

		if (firstTime) firstTime = false;
		else{

			#if UNITY_EDITOR

			if (Input.GetMouseButtonDown(0)) {

				Vector2 touchPosition = Input.mousePosition;

			#else
			
			if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Time.time > timeToFire)) {
			
				timeToFire = Time.time + 1/fireRate;

				Vector2 touchPosition = Input.GetTouch(0).position;

			#endif
				
				startShooting ();
				int i = selectRail(touchPosition, centerScreen);
				weapon.shoot(i);
			}
		}
	}

		void startShooting(){
			if (!shootingStarted){
				shootingStarted = true;
				anim.SetBool("shootingStarted", shootingStarted);
			}
	}

	int selectRail(Vector2 touchPosition, Vector2 centerScreen){

		int i = 0;

		if(touchPosition.x >= centerScreen.x && touchPosition.y >= centerScreen.y){
			x = true;
			y = true;
			i = 0;
		}else if (touchPosition.x <= centerScreen.x && touchPosition.y >= centerScreen.y){
			x = false;
			y = true;
			i = 1;
		}else if (touchPosition.x <= centerScreen.x && touchPosition.y <= centerScreen.y){
			x = false;
			y = false;
			i = 2;
		}else if (touchPosition.x >= centerScreen.x && touchPosition.y <= centerScreen.y){
			x = true;
			y = false;
			i = 3;
		}

		anim.SetBool("x", x);
		anim.SetBool("y", y);

		return(i);
	}
	
	void PlayerIsDead(Notification notification){
			NotificationCenter.DefaultCenter ().PostNotification (this, "UpdateGoogle");
			tag = "DeadPlayer";
			audio.Play();
			anim.Play ("PlayerIsDead");
			playerIsDead = true;
			anim.SetBool("playerIsDead", playerIsDead);
			thisScript.enabled = false;

	}

}
