using UnityEngine;
using System.Collections;

public class Destructor : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D Other){
		Destroy (Other.gameObject);
	}
}
