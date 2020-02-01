
using UnityEngine;

public class PlayerHands: MonoBehaviour, IPlayerComponent {
	public Vector2 getGroundCheck() {
		throw new System.NotImplementedException();
	}

	public void Enable() {
		gameObject.SetActive(true);
	}

	public void Disable() {
		gameObject.SetActive(false);
	}
}