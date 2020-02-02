using System.Collections.Generic;
using UnityEngine;

public class PlatformMover2: MonoBehaviour {

	[SerializeField] private Vector2 startPos;
	[SerializeField] private Vector2 endPos;
	[SerializeField] private List<Vector2> wayPoints = new List<Vector2>();
	[SerializeField] private bool snapToStart;

	private Transform _transform;
	private Vector2 _targetPos;
	private Vector2 _initPos;
	private int wayPointIndex;
	private bool _isMoving;
	
	[SerializeField][Range(0.0f, 10.0f)] private float moveSpeed = 5f; // enemy move speed
	
	private void Awake() {
		_transform = transform;
		_initPos = _transform.position;
	}

	private void Start() {
		if (wayPoints.Count > 1) _isMoving = true;
		_isMoving = true;
		wayPointIndex = 0;
		SetTarget();
	}

	private void SetTarget() {
		wayPointIndex = (wayPointIndex + 1) % wayPoints.Count;
		_targetPos    = new Vector2(_initPos.x + wayPoints[wayPointIndex].x, _initPos.y + wayPoints[wayPointIndex].y);
	}

	private void Update() {
		if (_isMoving) {
			Movement();	
		}
	}

	private void Movement() {
		_transform.position = Vector3.MoveTowards(_transform.position, _targetPos, moveSpeed * Time.deltaTime);
		if(Vector3.Distance(_targetPos, _transform.position) <= 0) {
			if (snapToStart && wayPointIndex == wayPoints.Count - 1) {
				wayPointIndex = 0;
				_transform.position = new Vector2(_initPos.x + wayPoints[wayPointIndex].x, _initPos.y + wayPoints[wayPointIndex].y);
			}
			SetTarget();
		}
	}
}