using UnityEngine;
using System.Collections;

public class VultureBehavior : MonoBehaviour {

	public Vector2 target = new Vector2 (0, 0);
	public bool x = true;
	public bool y = true;
	public AudioClip alternativeAudio;
	public float vultureSpeed = 0.5F;

	private bool stop = false;
	private Animator anim;

	void Start(){
		setTag();
		setAndPlaySound ();
		anim = GetComponent<Animator>();
		anim.SetBool("x", x);
		anim.SetBool("y", y);
	}

	void setTag(){
		if(x && y) tag = "Enemy0"; 
		else if(!x && y) tag = "Enemy1";
		else if(!x && !y) tag = "Enemy2";
		else if(x && !y) tag = "Enemy3";
	}

	void setAndPlaySound(){
		if (this.CompareTag("Enemy2") || this.CompareTag("Enemy3")) audio.clip = alternativeAudio;
		audio.Play();
	}

	void Stop(){
		stop = true;
		anim.SetBool("stop", stop);
	}

	void Die(){
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D Other){
		if(Other.tag == "Player") {
			NotificationCenter.DefaultCenter ().PostNotification (this, "PlayerIsDead");
		}
	}

	void Update(){
		if(!stop){
			transform.position = Vector3.Lerp (transform.position, target, 1 - Mathf.Pow(vultureSpeed, Time.deltaTime));
		}
	}

}
