using UnityEngine;

public class Loader: MonoBehaviour {
	// Singleton Object References
	[SerializeField] private GameObject gameManager;

	private void Awake() {
		if (GameManager.instance != null) return;

		Debug.Log("Instantiating game manager");
		Instantiate(gameManager);
	}
}