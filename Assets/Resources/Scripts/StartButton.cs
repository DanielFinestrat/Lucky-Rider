using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public PlayerController playerScript;
	public AudioSource horseSound;
	public Animator quadAnimator;
	public AudioSource mainTrack;
	public GameObject score;
	public GameObject clip;


	private AudioSource thisAudio;
	private BoxCollider2D thisBox;
	private Animator thisAnimator;

	public float timeToWait = 0.5f;
	public AudioClip alternativeAudio;

	void Start(){
		thisAnimator = GetComponent<Animator> ();
		thisAudio = GetComponent<AudioSource> ();
		thisBox = GetComponent<BoxCollider2D>();
	}

	void OnMouseDown(){
		thisBox.enabled = false;
		manageAudio();
		playAnimations();
		Invoke ("StartGame", audio.clip.length + timeToWait);
	}

	void playAnimations(){
		thisAnimator.Play ("StartExitScreen");
		quadAnimator.Play("FadeToTransparent");
		score.SetActive (true);
		clip.SetActive (true);
	}

	void manageAudio(){
		audio.clip = alternativeAudio;
		audio.Play ();
		thisAudio.loop = false;
	}

	void StartGame(){
		playerScript.enabled = true;
		horseSound.enabled = true;
		mainTrack.enabled = true;
		NotificationCenter.DefaultCenter ().PostNotification (this, "GameStarted");
		Destroy (this.gameObject);
	}

}
