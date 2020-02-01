using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class MovingPlatform: MonoBehaviour, IInteractableObject {
	[SerializeField] private Vector2 _moveDirection;
	
	// state variables
	// [SerializeField] private List<Transform> _attachedTransforms;
	[SerializeField] private bool _shouldMoveAttachments;
	[SerializeField] private List<MovingPlatform> _linkInteractableObjects;
	private bool _isActive;
	private float _power;
	
	// references
	private Transform _transform;

	private void Awake() {
		_transform = transform;
		// _attachedTransforms = new List<Transform>();
	}

	// #region IInteractableObject
	public void OnGearInput(Transform triggerTransform, float dt) {
		Debug.Log("Received input for cart");
		var deltaX = _moveDirection.x* dt;
		var deltaY = _moveDirection.y * dt;
		_transform.position = new Vector2(_transform.position.x+deltaX, _transform.position.y + deltaY);
		if (_shouldMoveAttachments) {
			triggerTransform.position = new Vector2(triggerTransform.position.x+deltaX, triggerTransform.position.y + deltaY);
		}
		foreach (var interactableObject in _linkInteractableObjects) {
			interactableObject.OnGearInput(_transform, dt);
		}
		
	}

	public void OnPowerInput() {
		throw new System.NotImplementedException();
	}

	public void OnRemoveSource() {
		_isActive = false;
		// _attachedTransforms.Clear();
	}

	public bool isInputAvailable(InteractableType type) {
		return type == InteractableType.GEAR;
	}

	// #endregion
	public void StartInteraction(GameObject obj, float power) {
		_isActive = true;
		_power = power;
		if (obj.tag == Constants.PlayerTag) {
			// _attachedTransforms.Add(obj.transform);
		}
	}

	public void StopInteraction() {
		_isActive = false;
		// _attachedTransforms.Clear();
	}
}