using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	public float Velocidad = 0f;

	void Update(){
		renderer.material.mainTextureOffset = new Vector2 (Time.time * Velocidad % 1, 0);
	}

}
