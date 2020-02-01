
using UnityEngine;

public class PlayerHands: MonoBehaviour, IPlayerComponent {
	public Transform getGroundCheck() {
		throw new System.NotImplementedException();
	}

	public void Enable() {
		gameObject.SetActive(true);
	}

	public void Disable() {
		gameObject.SetActive(false);
	}
}