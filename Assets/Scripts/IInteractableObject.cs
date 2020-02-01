using UnityEngine;

public interface IInteractableObject {
	void OnGearInput(Transform triggerTransform, float dt);
	void OnPowerInput();
	void OnRemoveSource();
	bool isInputAvailable(InteractableType type);
}

public interface IGearInput {
	void StartInteraction(GameObject obj, float power);
	void StopInteraction();
}

public interface IPowerInput {
	void StartInteraction();
	void StopInteraction();
}

public enum InteractableType {
	GEAR,
	POWER
}