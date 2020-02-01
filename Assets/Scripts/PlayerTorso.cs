
using UnityEngine;

public class PlayerTorso: MonoBehaviour, IPlayerComponent {
	
	[SerializeField] private GameObject groundCheckPosition;
	[SerializeField] private Animator _animator;

	private void Start() {
		_animator = GetComponent<Animator>();
		if (_animator == null) // if Animator is missing
			Debug.LogError($"Animator component missing from {gameObject.name}");
	}

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

	public void OnJumpTrigger() {
		if (!isActiveAndEnabled) return;
		_animator.SetBool("isGrounded", false);
	}

	public void OnJumpEnd() {
		if (!isActiveAndEnabled) return;
		_animator.SetBool("isGrounded", false);
	}
}