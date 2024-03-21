using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialBoneConstraintControl : MonoBehaviour

{
    public Transform[] legTargets;
    public GameObject[] legAimPositions;
    public float maxStepDistance;
    public AnimationCurve yMovementCurve;
    public AnimationCurve xMovementCurve;
    public AnimationCurve zMovementCurve;
    public float eccentricity = 1f;
    public float yVectorLimit = 2f;
    public float xVectorLimit = 2f;
    public float zVectorLimit = 2f;
    public float stepDuration = 1f;

    private int currentLegIndex = 0;
    private bool isCoroutineRunning = false;

    void Start()
    {
        for (int i = 0; i < legAimPositions.Length; i++)
        {
            legAimPositions[i].SetActive(false);
        }
    }

    void Update()
    {
        if (!isCoroutineRunning)
        {
            SetIKPosition();
        }
    }

    private void SetIKPosition()
    {
        GameObject currentAimPosition = legAimPositions[currentLegIndex];
        float currentStepDistance = Vector3.Distance(transform.position, currentAimPosition.transform.position);

        if (currentStepDistance > maxStepDistance)
        {
            StartCoroutine(MoveLegOverTime(currentAimPosition.transform.position));
        }
    }

    IEnumerator MoveLegOverTime(Vector3 targetPosition)
    {
        isCoroutineRunning = true;

        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < stepDuration)
        {
            float progress = (Time.time - startTime) / stepDuration;

            float yStepPosition = yMovementCurve.Evaluate(progress) * yVectorLimit;
            float xStepPosition = xMovementCurve.Evaluate(progress) * xVectorLimit * eccentricity;
            float zStepPosition = zMovementCurve.Evaluate(progress) * zVectorLimit * eccentricity;

            Vector3 lerpedPosition = Vector3.Lerp(startPosition, targetPosition, progress);
            Vector3 offset = new Vector3(xStepPosition, yStepPosition, zStepPosition);
            transform.position = lerpedPosition + offset;

            yield return null;
        }

        transform.position = targetPosition;
        isCoroutineRunning = false;

        currentLegIndex++;
        if (currentLegIndex >= legAimPositions.Length)
        {
            currentLegIndex = 0; // Reset index to loop through the legs
        }
    }
}


