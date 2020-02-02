using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class MovingPlatform: MonoBehaviour, IInteractableObject {
	
	// config
	[SerializeField] private Vector2 _moveDirection; // Can also be used for speed
	[SerializeField] private float clockwiseDistance;
	[SerializeField] private float anticlockDistance;

	// state variables
	// [SerializeField] private List<Transform> _attachedTransforms;
	[SerializeField] private bool                 _shouldMoveAttachments;
	[SerializeField] private List<MovingPlatform> _linkInteractableObjects;
	[EnumFlags]      public  InteractableType     _allowedInteractions;

	private bool  _isActive;
	private float _power;

	// references
	private Transform _transform;
	private Vector2 _initPos;

	private void Awake() {
		_transform = transform;
		_initPos = new Vector2(_transform.position.x, _transform.position.y);
		// _attachedTransforms = new List<Transform>();
	}

	// #region IInteractableObject
	public void OnGearInput(Transform triggerTransform, float dt, bool isClockwise) {
		// Debug.Log("Received input for cart");
		var mult   = isClockwise ? 1 : -1;
		var deltaX = _moveDirection.x * dt * mult;
		var deltaY = _moveDirection.y * dt * mult;
		
		var newPos = new Vector2(_transform.position.x + deltaX, _transform.position.y + deltaY);
		var distance = Vector2.Distance(newPos, _initPos);
		var isC = 0f;
		if (Math.Abs(_moveDirection.x) > 0.01) {
			isC = (newPos.x - _initPos.x) / _moveDirection.x;
		} else if(Math.Abs(_moveDirection.y) > 0.01) {
			isC = (newPos.y - _initPos.y) / _moveDirection.y;
		}
		if (isC > 0 && distance > clockwiseDistance || isC < 0 && distance > anticlockDistance) {
			return;
		}
		foreach (var interactableObject in _linkInteractableObjects) {
			interactableObject.OnGearInput(_transform, dt, !isClockwise);
		}
		_transform.position = newPos;
		if (_shouldMoveAttachments) {
			triggerTransform.position = new Vector2(triggerTransform.position.x + deltaX, triggerTransform.position.y + deltaY);
		}
	}

	public void OnPowerInput() {
		throw new System.NotImplementedException();
	}

	public void OnRemoveSource() {
		_isActive = false;
		// _attachedTransforms.Clear();
	}

	public bool CanInteract(InteractableType type) {
		return (_allowedInteractions & type) != InteractableType.NONE;
	}

	// #endregion
	public void UpdateDirection(Vector2 direction, float cDistance, float aDistance) {
		_initPos = new Vector2(_transform.position.x, _transform.position.y);
		_moveDirection = direction;
		clockwiseDistance = cDistance;
		anticlockDistance = aDistance;
	}
}