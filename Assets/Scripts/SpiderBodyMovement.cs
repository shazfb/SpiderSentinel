using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpiderBodyMovement : MonoBehaviour
{
    public Transform frontLeftIKTarget;
    public Transform frontRightIKTarget;
    public Transform rearLeftIKTarget;
    public Transform rearRightIKTarget;
    public GameObject spiderBody;

    public float bodyHeightOffset = 1f;

    public float adjustmentSpeed = 1f;
    public float rotationSpeed = 5f;

    void Update()
    {
        SetBodyHeight();
        RotateBody();

    }

    public void SetBodyHeight()
    {
        float avgLegHeight = (frontLeftIKTarget.transform.position.y + frontRightIKTarget.transform.position.y + rearLeftIKTarget.transform.position.y + rearRightIKTarget.transform.position.y) / 4f;

        spiderBody.transform.position = Vector3.Lerp(spiderBody.transform.position, new Vector3(transform.position.x, avgLegHeight + bodyHeightOffset, transform.position.z), adjustmentSpeed);
    }

    public void RotateBody()
    {
        Vector3 avgRearLegPosition = (rearLeftIKTarget.transform.position + rearRightIKTarget.transform.position) / 2f;

        Vector3 vector1 = frontRightIKTarget.transform.position - frontLeftIKTarget.transform.position;
        Vector3 vector2 = avgRearLegPosition - frontLeftIKTarget.transform.position;

        Vector3 normal = Vector3.Cross(vector1, vector2).normalized;

        Debug.DrawRay(transform.position, normal, Color.blue);

        Quaternion targetRotation = Quaternion.FromToRotation(spiderBody.transform.up, normal) * spiderBody.transform.rotation;
        spiderBody.transform.rotation = Quaternion.Slerp(spiderBody.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
