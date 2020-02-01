using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform: MonoBehaviour, IInteractableObject {
	[SerializeField] private Vector2 _moveDirection;
	
	// state variables
	[SerializeField] private List<Transform> _attachedTransforms;
	private bool _isActive;
	private float _power;
	
	// references
	private Transform _transform;

	private void Awake() {
		_transform = transform;
		_attachedTransforms = new List<Transform>();
	}

	// #region IInteractableObject
	public void OnGearInput(Transform triggerTransform, float dt) {
		Debug.Log("Received input for cart");
		var deltaX = _moveDirection.x* dt;
		var deltaY = _moveDirection.y * dt;
		_transform.position = new Vector2(_transform.position.x+deltaX, _transform.position.y + deltaY);
		triggerTransform.position = new Vector2(triggerTransform.position.x+deltaX, triggerTransform.position.y + deltaY);
	}

	public void OnPowerInput() {
		throw new System.NotImplementedException();
	}

	public void OnRemoveSource() {
		throw new System.NotImplementedException();
	}

	public bool isInputAvailable(InteractableType type) {
		return type == InteractableType.GEAR;
	}

	// #endregion
	public void StartInteraction(GameObject obj, float power) {
		_isActive = true;
		_power = power;
		if (obj.tag == Constants.PlayerTag) {
			_attachedTransforms.Add(obj.transform);
		}
	}

	public void StopInteraction() {
		_isActive = false;
		_attachedTransforms.Clear();
	}

	private void Update() {
		if (!_isActive) return;
		var deltaX = _moveDirection.x * _power;
		var deltaY = _moveDirection.x * _power;
		var newPosition =
			new Vector2(_transform.position.x + _moveDirection.x * _power, _transform.position.y + _power);
		_transform.position = newPosition;
		foreach (var t in _attachedTransforms) {
			t.position = new Vector2(t.position.x + deltaX, t.position.y + deltaY);
		}
	}
}