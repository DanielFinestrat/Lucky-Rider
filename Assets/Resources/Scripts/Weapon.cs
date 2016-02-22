using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject[] point;
	public GameObject projectile;
	public AudioClip noBullets;

	private AudioSource thisAudio;
	private bool areBulletsLeft = true;

	void Start(){
		thisAudio = GetComponent<AudioSource> ();
		NotificationCenter.DefaultCenter().AddObserver(this, "NoBulletsLeft");
	}

	public void shoot(int i){

		if (areBulletsLeft){
			Vector2 targetPosition = obtainTarget(i);

			GameObject newProjectile = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
			BulletBehavior bulletbehavior = newProjectile.GetComponent<BulletBehavior>();

			bulletbehavior.target = targetPosition;
			bulletbehavior.kind = i;
		}else{
			thisAudio.clip = noBullets;
		}

		thisAudio.Play ();

	}

	Vector2 obtainTarget(int i){

		Vector2 targetPosition = new Vector2 (0, 0);

		if (i == 0) {
			targetPosition = new Vector2 (point[i].transform.position.x, point[i].transform.position.y);
		}else if (i==1){
			targetPosition = new Vector2 (point[i].transform.position.x, point[i].transform.position.y); 
		}else if (i==2){
			targetPosition = new Vector2 (point[i].transform.position.x, point[i].transform.position.y); 
		}else if (i==3){
			targetPosition = new Vector2 (point[i].transform.position.x, point[i].transform.position.y); 
		}

		return (targetPosition);
	}

	void NoBulletsLeft(Notification notification){
		areBulletsLeft = false;
	}

}
