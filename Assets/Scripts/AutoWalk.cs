using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWalk : MonoBehaviour
{

    public float movementSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
