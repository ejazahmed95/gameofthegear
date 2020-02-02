using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInOut : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public Vector2 offsetVector;
    public AnimationCurve curve;

    private Transform initialTransform;
    private float currTime = 0;
    private bool movingBack = true;
    void Start()
    {
        initialTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime < time && movingBack)
        {
            transform.position = new Vector3(initialTransform.position.x + (offsetVector.x * curve.Evaluate(currTime/time)), (initialTransform.position.y + offsetVector.y * curve.Evaluate(currTime/time)), initialTransform.position.z);
            currTime += Time.deltaTime;
        }
        else if (currTime < time)
        {
            movingBack = false;
            currTime = 0;
            transform.position = new Vector3(initialTransform.position.x + offsetVector.x - (offsetVector.x * curve.Evaluate(currTime/time)), initialTransform.position.y + offsetVector.y - (offsetVector.y * curve.Evaluate(currTime/time)), initialTransform.position.z);
            currTime += Time.deltaTime;
        }
        else
            movingBack = !movingBack;
    }
}
