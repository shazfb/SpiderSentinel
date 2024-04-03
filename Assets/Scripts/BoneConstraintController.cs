using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoneConstraintController : MonoBehaviour
 {
    public GameObject legAimPosition;
    private float currentStepDistance;
    private Vector3 currentIKPos;

    public float maxStepDistance;

    public BoneConstraintController oppositeLeg;

    public AnimationCurve yMovementCurve;

    public AnimationCurve xMovementCurve;
    public AnimationCurve zMovementCurve;

    public bool stepOffset = true;

    [Range(0, 1f)] public float eccentricity = 1f;

    public float yVectorLimit = 2f;
    public float xVectorLimit = 2f;
    public float zVectorLimit = 2f;

    public float stepDuration = 1f;
    private float startTime;

    private bool isCoroutineRunning = false;

    void Start()
    {
        currentIKPos = transform.position;
        legAimPosition.GetComponent<MeshRenderer>().enabled = false;
        if (stepOffset)
        {
            StartCoroutine(HalfStep());
        }
    }

    void Update()
    {
        SetIKPosition();        
    }


    public bool CheckIsMoving()
    {
        return isCoroutineRunning;
    }

    private void SetIKPosition()
    {
        currentStepDistance = Vector3.Distance(currentIKPos,legAimPosition.transform.position);

        if (currentStepDistance > maxStepDistance && !isCoroutineRunning && !oppositeLeg.CheckIsMoving())
        {
           StartCoroutine (MoveLegOverTime());

        }
        else
        {
           transform.position = currentIKPos;
        }
    }

    public IEnumerator MoveLegOverTime()
    {
        Debug.Log("Coroutine started");
        isCoroutineRunning = true;

        startTime = Time.time;

        while (Time.time - startTime < stepDuration)
        {
            float progress = (Time.time - startTime) / stepDuration;

            float yStepPosition = yMovementCurve.Evaluate(progress) * yVectorLimit;
            float xStepPosition = xMovementCurve.Evaluate(progress) * xVectorLimit * eccentricity;
            float zStepPosition = zMovementCurve.Evaluate(progress) * zVectorLimit * eccentricity;
            Vector3 legLerpPosition = Vector3.Lerp(transform.position, legAimPosition.transform.position, progress);

            transform.position = new Vector3(legLerpPosition.x + xStepPosition, legLerpPosition.y + yStepPosition, legLerpPosition.z + zStepPosition);

            yield return null;
        }

        transform.position = legAimPosition.transform.position;
        currentIKPos = legAimPosition.transform.position;

        isCoroutineRunning = false;

    }

    IEnumerator HalfStep()
    {
        Debug.Log("HalfStep Coroutine started");
        isCoroutineRunning = true;

        startTime = Time.time;

        while (Time.time - startTime < stepDuration)
        {
            float progress = (Time.time - startTime) / stepDuration;

            float yStepPosition = yMovementCurve.Evaluate(progress) * yVectorLimit/2;
            float xStepPosition = xMovementCurve.Evaluate(progress) * xVectorLimit * eccentricity;
            float zStepPosition = zMovementCurve.Evaluate(progress) * zVectorLimit * eccentricity;
            Vector3 legLerpPosition = Vector3.Lerp(transform.position, legAimPosition.transform.position, progress);

            transform.position = new Vector3(legLerpPosition.x + xStepPosition, legLerpPosition.y + yStepPosition, legLerpPosition.z + zStepPosition)/2;

            yield return null;
        }

        transform.position = legAimPosition.transform.position;
        currentIKPos = legAimPosition.transform.position;

        isCoroutineRunning = false;
        stepOffset = false;

    }
}
