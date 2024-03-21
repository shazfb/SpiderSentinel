using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StepAnimation : MonoBehaviour
{
    public Transform position1;
    public Transform position2;

    public float maxDistance = 3;
    public float currentDistance;
    private float startTime;

    public float stepDuration = 1f;

    private bool isCoroutineRunning = false;

    public AnimationCurve yMovementCurve;

    public AnimationCurve xMovementCurve;
    public AnimationCurve zMovementCurve;

    [Range(0, 1f)] public float eccentricity = 1f;

    public float yVectorLimit = 2f;
    public float xVectorLimit = 2f;
    public float zVectorLimit = 2f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = position1.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(position1.position, position2.position);
        if (currentDistance > maxDistance && !isCoroutineRunning)
        {
            StartCoroutine(MoveLegOverTime());
        }
    }

    IEnumerator MoveLegOverTime()
    {
        isCoroutineRunning = true;

        startTime = Time.time;

        while (Time.time - startTime < stepDuration)
        {
            float progress = (Time.time - startTime) / stepDuration;

            float yStepPosition = yMovementCurve.Evaluate(progress) * yVectorLimit;
            float xStepPosition = xMovementCurve.Evaluate(progress) * xVectorLimit * eccentricity;
            float zStepPosition = zMovementCurve.Evaluate(progress) * zVectorLimit * eccentricity;

            Vector3 legLerpPosition = Vector3.Lerp(position1.position,position2.position, progress);

            transform.position = new Vector3(legLerpPosition.x + xStepPosition, legLerpPosition.y + yStepPosition, legLerpPosition.z + zStepPosition);

            yield return null;
        }

        transform.position = position2.position;
        position1.position = position2.position;

        isCoroutineRunning=false;

        
    }
}
