using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	[SerializeField] private GameObject head;
	[SerializeField] private GameObject torso;
	[SerializeField] private GameObject hands;
	[SerializeField] private GameObject wheels;

	// State
	[SerializeField] public PlayerComponentType _components = PlayerComponentType.HEAD;

	// References


	private void Start() {
		EnableComponents();
	}

	private void EnableComponents() {
		if ((_components & PlayerComponentType.HEAD) == PlayerComponentType.NONE) {
			// Disable collider for head
			head.SetActive(false);
		} 
		if ((_components & PlayerComponentType.TORSO) == PlayerComponentType.NONE) {
			// Disable collider for head
			torso.SetActive(false);
		} 
		if ((_components & PlayerComponentType.HANDS) == PlayerComponentType.NONE) {
			// Disable collider for head
			hands.SetActive(false);
		} 
		if ((_components & PlayerComponentType.WHEELS) == PlayerComponentType.NONE) {
			// Disable collider for head
			wheels.SetActive(false);
		}
	}
	
}