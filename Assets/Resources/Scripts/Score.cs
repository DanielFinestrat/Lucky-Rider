using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	private int key = 0;
	private int _score = 0;
	public int score{
		get{ return _score ^ key; }
		set{ 
			key = Random.Range(0, int.MaxValue);
			_score = value ^ key;
		}
	}

	public int pointsPerDeath = 1;
	private TextMesh marcador;

	private bool counting = true;

	void Start () {
		marcador = GetComponent<TextMesh> ();
		NotificationCenter.DefaultCenter ().AddObserver (this, "AddPoint");
		NotificationCenter.DefaultCenter ().AddObserver (this, "PlayerIsDead");
		NotificationCenter.DefaultCenter ().PostNotification (this, "ScoreEnabled");
	}

	void ActualizarMarcador(){
		marcador.text = score.ToString();
	}

	void PlayerIsDead(Notification notification){
		counting = false;
	}

	void AddPoint(Notification notification){
		if(counting){
			score = score + pointsPerDeath;
			ActualizarMarcador();
		}
	}

}
