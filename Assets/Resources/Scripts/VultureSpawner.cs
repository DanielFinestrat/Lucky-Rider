using UnityEngine;
using System.Collections;

public class VultureSpawner : MonoBehaviour {

	private bool firstTime = true;
	private bool keepSpawning = true;

	public GameObject vulture;
	public bool x = true;
	public bool y = true;

	public float tiempoMin = 1f;
	public float tiempoMax = 2f;

	public GameObject[] target;

	void Start(){
		NotificationCenter.DefaultCenter ().AddObserver (this, "GameStarted");
		NotificationCenter.DefaultCenter ().AddObserver (this, "PlayerIsDead");
	}

	void GameStarted(Notification notification){
		Generar();
	}

	void Generar(){
		if (firstTime) firstTime = false;

		else if (keepSpawning){
			GameObject newVulture =  Instantiate (vulture, transform.position, transform.rotation) as GameObject;
			VultureBehavior vultureBehavior = newVulture.GetComponent<VultureBehavior>();

			vultureBehavior.target = selectVultureTarget();
			vultureBehavior.x = x;
			vultureBehavior.y = y;
		}

		Invoke ("Generar", Random.Range (tiempoMin, tiempoMax));
	}

	Vector2 selectVultureTarget(){

		Vector2 vultureTarget = new Vector2 (0, 0);

		if(x && y) vultureTarget = target[0].transform.position;
		else if (!x && y) vultureTarget = target[1].transform.position;
		else if (!x && !y) vultureTarget = target[2].transform.position;
		else if (x && !y) vultureTarget = target[3].transform.position;

		return(vultureTarget);
	}

	void PlayerIsDead(Notification notification){
		keepSpawning = false;
	}

}
