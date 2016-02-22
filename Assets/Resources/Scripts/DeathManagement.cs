using UnityEngine;
using System.Collections;

public class DeathManagement : MonoBehaviour {
	
	public GameObject gameOverButtons;
	public Animator blackQuadAnimator;
	public Animator score;

	void Start () {
		NotificationCenter.DefaultCenter ().AddObserver (this, "PlayerIsDead");
	}
	
	void PlayerIsDead(Notification notification) {
		audio.Stop ();
		score.Play ("DeathPoints");
		blackQuadAnimator.Play ("FadeToBlack");
		gameOverButtons.SetActive (true);

	}
}
