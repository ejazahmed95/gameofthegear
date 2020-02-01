
using UnityEngine;

public class PlayerWheels: MonoBehaviour, IPlayerComponent {
	[SerializeField] private Transform groundCheckPosition;
	
	public Transform getGroundCheck() {
		// Store this variable beforehand
		return groundCheckPosition;
	}

	public void Enable() {
		gameObject.SetActive(true);
	}

	public void Disable() {
		gameObject.SetActive(false);
	}

	public void onMoving(float f) {
		// Run moving animation where f is the speed
	}
}