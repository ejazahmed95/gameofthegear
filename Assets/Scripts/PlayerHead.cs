
using UnityEngine;

public class PlayerHead: MonoBehaviour, IPlayerComponent {

	[SerializeField] private Transform groundCheckPosition;
	[SerializeField] private IInteractableObject _interactableObject;
	
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

	public void StartInteraction(IInteractableObject obj) {
		_interactableObject = obj;
	}

	public void EndInteraction() {
		_interactableObject = null;
	}

	public void OnInteractionInput(float vx) {
		if (!isActiveAndEnabled || _interactableObject == null) return;
		_interactableObject.OnGearInput();
	}
}