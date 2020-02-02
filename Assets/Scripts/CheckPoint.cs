using UnityEngine;

public class CheckPoint: MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Entered into checkpoint without tag");
		if (other.CompareTag(Constants.PlayerTag)) {
			Debug.Log("Entered into checkpoint");
			GameManager.instance.SetLastCheckpoint(transform.position);	
		}
	}
}