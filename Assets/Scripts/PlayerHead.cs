
using UnityEngine;

public class PlayerHead: MonoBehaviour, IPlayerComponent {

	[SerializeField] private GameObject groundCheckPosition;
	
	public Vector2 getGroundCheck() {
		// Store this variable beforehand
		return groundCheckPosition.transform.position;
	}

	public void Enable() {
		gameObject.SetActive(true);
	}

	public void Disable() {
		gameObject.SetActive(false);
	}
}