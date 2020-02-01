using UnityEngine;

public interface IPlayerComponent {
	Transform getGroundCheck();
	void Enable();
	void Disable();
}