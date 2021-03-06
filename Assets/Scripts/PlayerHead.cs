
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
		_interactableObject.OnRemoveSource();
		_interactableObject = null;
	}

	public void OnInteractionInput(Transform parentTransform, float dt, float vx) {
		if (!isActiveAndEnabled || _interactableObject == null) return;
		var isClockwise = !(vx > 0);
		_interactableObject.OnGearInput(parentTransform, dt, isClockwise);
	}
}