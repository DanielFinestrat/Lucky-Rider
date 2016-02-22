using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Button : MonoBehaviour {

	public bool again = true;
	public bool exit = false;
	public bool ranking = false;
	public bool badges = false;

	private GameObject disabledChild;

	void Update(){

		if((ranking || badges) && Social.localUser.authenticated){
			disabledChild = transform.Find("Disabled").gameObject;
			disabledChild.SetActive(false);
		}else if ((ranking || badges) && !Social.localUser.authenticated){
			disabledChild = transform.Find("Disabled").gameObject;
			disabledChild.SetActive(true);
		}

	}

	void OnMouseDown(){

		if (again) againAction();
		else if (exit) exitAction();
		else if (ranking) rankingAction();
		else if (badges) badgesAction();

	}

	void againAction(){
		NotificationCenter.DefaultCenter ().PostNotification (this, "Again");
		Application.LoadLevel ("GameScene");
	}

	void exitAction(){
		Application.Quit ();
	}

	void rankingAction(){
		if(Social.localUser.authenticated){
			((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI0oKVh6oBEAIQAA");
		}else{
			Social.localUser.Authenticate((bool success) => {
				if(success){
					NotificationCenter.DefaultCenter().PostNotification(this, "Again");
					NotificationCenter.DefaultCenter().PostNotification(this, "UpdateGoogle");
					((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI0oKVh6oBEAIQAA");
				}
			});
		}
	}

	void badgesAction(){
		if(Social.localUser.authenticated){
			((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
		}else{
			Social.localUser.Authenticate((bool success) => {
				if(success){
					NotificationCenter.DefaultCenter().PostNotification(this, "Again");
					NotificationCenter.DefaultCenter().PostNotification(this, "UpdateGoogle");
					((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
				}
			});
		}
	}

}
