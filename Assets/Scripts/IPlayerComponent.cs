using UnityEngine;

public interface IPlayerComponent {
	Vector2 getGroundCheck();
	void Enable();
	void Disable();
}