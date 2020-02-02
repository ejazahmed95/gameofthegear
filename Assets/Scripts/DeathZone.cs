using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	public bool destroyNonPlayerObjects = true;

	// Handle gameobjects collider with a deathzone object
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			GameManager.instance.OnPlayerDead();
		} else if (destroyNonPlayerObjects) { // not playe so just kill object - could be falling enemy for example
			DestroyObject(other.gameObject);
		}
	}
}
