using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

	public Vector2 target = new Vector3 (0, 0, 0);
	public float projectileSpeed = 0.5F;
	public int kind;
	public Animator enemyAnimator;
	public float maxDistance = 2f;

	private bool isToDestroy = false;

	void Start(){
		if(kind == 0) tag = "Bullet0";
		else if(kind == 1) tag = "Bullet1";
		else if(kind == 2) tag = "Bullet2";
		else if(kind == 3) tag = "Bullet3";
	}

	void Update(){
		transform.position = Vector3.Lerp (transform.position, target, 1 - Mathf.Pow(projectileSpeed, Time.deltaTime));
		DeleteOnMaxDistance ();
	}

	void DeleteOnMaxDistance(){
		float spawnerDistanceX = target.x;
		float positionX = transform.position.x;

		if(spawnerDistanceX < 0) spawnerDistanceX = spawnerDistanceX * -1;
		if(positionX < 0) positionX = positionX * -1;

		if(positionX >= spawnerDistanceX - maxDistance){
			Destroy(this.gameObject);
			NotificationCenter.DefaultCenter().PostNotification(this, "StrayBullet");
		}


	}

	void OnTriggerEnter2D(Collider2D Other){
		if(tag=="Bullet0" && Other.tag == "Enemy0") isToDestroy = true;
		else if(tag=="Bullet1" && Other.tag == "Enemy1") isToDestroy = true;
		else if(tag=="Bullet2" && Other.tag == "Enemy2") isToDestroy = true;
		else if(tag=="Bullet3" && Other.tag == "Enemy3") isToDestroy = true;

		if (isToDestroy) {
			NotificationCenter.DefaultCenter().PostNotification(this, "AddPoint");
			enemyAnimator = Other.gameObject.GetComponent<Animator>();
			enemyAnimator.Play("DeathStar");
			Destroy(this.gameObject);
		}
	}

	void PlayerIsDead(Notification notification){
		Destroy (this.gameObject);
	}

}
