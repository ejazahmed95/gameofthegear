using System;
using UnityEngine;

public interface IInteractableObject {
	void OnGearInput(Transform triggerTransform, float dt, bool isClockwise);
	void OnPowerInput();
	void OnRemoveSource();
	bool CanInteract(InteractableType type);
}

public interface IGearInput {
	void StartInteraction(GameObject obj, float power);
	void StopInteraction();
}

public interface IPowerInput {
	void StartInteraction();
	void StopInteraction();
}

[Flags]
public enum InteractableType {
	NONE  = 0,
	GEAR  = 1 << 1,
	POWER = 1 << 2
}