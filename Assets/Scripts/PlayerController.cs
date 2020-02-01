using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[Flags]
public enum PlayerComponentType {
	NONE   = 0,
	HEAD   = 1 << 1,
	TORSO  = 1 << 2,
	HANDS  = 1 << 3,
	WHEELS = 1 << 4
}

public class PlayerController: MonoBehaviour {
	// Config
	[SerializeField] private PlayerHead head;
	[SerializeField] private PlayerTorso torso;
	[SerializeField] private PlayerHands hands;
	[SerializeField] private PlayerWheels wheels;

	[Range(0.0f, 10.0f)] // create a slider in the editor and set limits on moveSpeed
	[SerializeField]
	private float moveSpeed = 3f;

	[SerializeField] private float     jumpForce = 600f;
	[SerializeField] private LayerMask whatIsGround;

	// State
	[SerializeField]  public PlayerComponentType _components   = PlayerComponentType.HEAD;
	[HideInInspector] public bool                playerCanMove = true;

	private Vector2 _groundCheck;

	// hold player motion in this timestep
	float _vx;
	float _vy;

	// player tracking
	bool _facingRight = true;
	bool _isGrounded  = false;

	int _playerLayer;
	int _platformLayer; // number of layer that Platforms are on (setup in Awake)

	// References
	private Transform   _transform;
	private Rigidbody2D _rigidbody;
	private Animator    _animator;
	private AudioSource _audio;

	private void Awake() {
		RegisterReferences();
	}

	private void Start() {
		EnableComponents();
	}

	// this is where most of the player controller magic happens each game event loop
	void Update() {
		// exit update if player cannot move or game is paused
		if (!playerCanMove || (Time.timeScale == 0f))
			return;

		// determine horizontal velocity change based on the horizontal input
		_vx = CrossPlatformInputManager.GetAxisRaw("Horizontal");

		// Determine if running based on the horizontal movement
		// if (_vx != 0) {
		// 	_isRunning = true;
		// } else {
		// 	_isRunning = false;
		// }

		// set the running animation state
		// _animator.SetBool("Running", _isRunning);

		// get the current vertical velocity from the rigidbody component
		_vy = _rigidbody.velocity.y;

		// Check to see if character is grounded by raycasting from the middle of the player
		// down to the groundCheck position and see if collected with gameobjects on the
		// whatIsGround layer 
		_isGrounded = Physics2D.Linecast(_transform.position, _groundCheck, whatIsGround);

		// Set the grounded animation states
		// _animator.SetBool("Grounded", _isGrounded);

		if (_isGrounded && CrossPlatformInputManager.GetButtonDown("Jump")) {
			// If grounded AND jump button pressed, then allow the player to jump
			DoJump();
		}

		// If the player stops jumping mid jump and player is not yet falling
		// then set the vertical velocity to 0 (he will start to fall from gravity)
		if (CrossPlatformInputManager.GetButtonUp("Jump") && _vy > 0f) {
			_vy = 0f;
		}

		// Change the actual velocity on the rigidbody
		_rigidbody.velocity = new Vector2(_vx * moveSpeed, _vy);

		// if moving up then don't collide with platform layer
		// this allows the player to jump up through things on the platform layer
		// NOTE: requires the platforms to be on a layer named "Platform"
		Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, _vy > 0.0f);
	}
	
	// Checking to see if the sprite should be flipped
	// this is done in LateUpdate since the Animator may override the localScale
	// this code will flip the player even if the animator is controlling scale
	void LateUpdate()
	{
		// get the current scale
		Vector3 localScale = _transform.localScale;

		if (_vx > 0) // moving right so face right
		{
			_facingRight = true;
		} else if (_vx < 0) { // moving left so face left
			_facingRight = false;
		}

		// check to see if scale x is right for the player
		// if not, multiple by -1 which is an easy way to flip a sprite
		if (_facingRight && localScale.x<0 || !_facingRight && localScale.x>0) {
			localScale.x *= -1;
		}

		// update the scale
		_transform.localScale = localScale;
	}
	
	private void DoJump() {
		// reset current vertical motion to 0 prior to jump
		_vy = 0f;
		// add a force in the up direction
		_rigidbody.AddForce (new Vector2 (0, jumpForce));
		// play the jump sound
		// PlaySound(jumpSFX);
	}

	#region Initial Setup

	private void RegisterReferences() {
		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		_transform = GetComponent<Transform>();

		_rigidbody = GetComponent<Rigidbody2D>();
		if (_rigidbody == null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");

		// _animator = GetComponent<Animator>();
		// if (_animator == null) // if Animator is missing
		// 	Debug.LogError("Animator component missing from this gameobject");

		_audio = GetComponent<AudioSource>();
		if (_audio == null) { // if AudioSource is missing
			Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}

		// determine the player's specified layer
		_playerLayer = gameObject.layer;

		// determine the platform's specified layer
		_platformLayer = LayerMask.NameToLayer("Platform");
	}

	private void EnableComponents() {
		head.Disable();
		torso.Disable();
		hands.Disable();
		wheels.Disable();
		_groundCheck = _transform.position;
		if ((_components & PlayerComponentType.HEAD) != PlayerComponentType.NONE) {
			// Disable collider for head
			head.Enable();
			_groundCheck = head.getGroundCheck();
		}
		if ((_components & PlayerComponentType.TORSO) != PlayerComponentType.NONE) {
			// Disable collider for head
			torso.Enable();
			_groundCheck = torso.getGroundCheck();
		}
		if ((_components & PlayerComponentType.HANDS) != PlayerComponentType.NONE) {
			// Disable collider for head
			hands.Enable();
			// _groundCheck = hands.getGroundCheck();
		}
		if ((_components & PlayerComponentType.WHEELS) != PlayerComponentType.NONE) {
			// Disable collider for head
			wheels.Enable();
			_groundCheck = wheels.getGroundCheck();
		}
	}

	#endregion
}