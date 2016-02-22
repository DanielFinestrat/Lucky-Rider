using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour {

	public static GooglePlayManager mainManager;
	
	private Score myScore;

	private bool firstTime = true;

	void Awake(){
		if(mainManager == null){
			mainManager = this;
			DontDestroyOnLoad(gameObject);
			PlayGamesPlatform.Activate();
		}else Destroy(gameObject);
	}

	void Start(){
		((PlayGamesPlatform)Social.Active).Authenticate((bool success) => {if(!success && PlayerPrefs.GetInt("DoNotAsk") != 1) logIn();}, true);
		NotificationCenter.DefaultCenter ().AddObserver (this, "ScoreEnabled");
		NotificationCenter.DefaultCenter ().AddObserver (this, "UpdateGoogle");
		NotificationCenter.DefaultCenter ().AddObserver (this, "Again");
	}

	void Again(){
		firstTime = true;
	}

	void logIn(){
		Social.localUser.Authenticate((bool success) => {});
		PlayerPrefs.SetInt ("DoNotAsk", 1);
		PlayerPrefs.Save ();
	}
	
	void UpdateGoogle(Notification notification){
		if(firstTime){
			#if !UNITY_EDITOR
				firstTime = false;
				Social.ReportScore (myScore.score, "CgkI0oKVh6oBEAIQAA", (bool success) => {});
				manageBadges ();
			#endif
		}
	}
	void ScoreEnabled(Notification notification){
		myScore = GameObject.Find("Points").gameObject.GetComponent<Score>();
	}
	
	void manageBadges(){
		manageSurvivor ();
		managePlayer ();
		manageHunter ();
	}

	void manageSurvivor(){
		if (myScore.score >= 20) Social.ReportProgress("CgkI0oKVh6oBEAIQAg", 100.0, (bool success) => {});
		if (myScore.score >= 40) Social.ReportProgress("CgkI0oKVh6oBEAIQBQ", 100.0, (bool success) => {});
		if (myScore.score >= 80) Social.ReportProgress("CgkI0oKVh6oBEAIQCA", 100.0, (bool success) => {});
		if (myScore.score >= 100) Social.ReportProgress("CgkI0oKVh6oBEAIQCw", 100.0, (bool success) => {});
		if (myScore.score >= 150) Social.ReportProgress("CgkI0oKVh6oBEAIQDg", 100.0, (bool success) => {});
	}

	void managePlayer(){

		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQAw", 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQBg", 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQCQ", 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQDA", 1, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQDw", 1, (bool success) => {});
	}

	void manageHunter(){
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQEQ", myScore.score, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQBw", myScore.score, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQCg", myScore.score, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQDQ", myScore.score, (bool success) => {});
		PlayGamesPlatform.Instance.IncrementAchievement("CgkI0oKVh6oBEAIQEA", myScore.score, (bool success) => {});
	}

}
