using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour {
    private float CurrTime = 0;

    [SerializeField]private float _durationOn = 2f;

    [SerializeField]private float _durationOff = 2f;

    [SerializeField] private SpriteRenderer _sprite;

    [SerializeField] private GameObject deathZone;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(StartLightning());
    }

    IEnumerator StartLightning() {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        _sprite.enabled = true;
        yield return new WaitForSeconds(_durationOn);
        StartCoroutine(StopLightning());
    }

    IEnumerator StopLightning() {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _sprite.enabled                              = false;
        yield return new WaitForSeconds(_durationOff);
        StartCoroutine(StartLightning());
    }
    
   
}
