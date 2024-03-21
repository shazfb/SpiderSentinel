using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegGrounding : MonoBehaviour
{
    private int layerMask;
    public Vector3 groundingOffset = new Vector3(0, 0.75f, 0f);
    public GameObject raycastOrigin;


    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        raycastOrigin = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.transform.position + groundingOffset, -transform.up, out hit, Mathf.Infinity, layerMask))
        {
            transform.position = hit.point + groundingOffset;
        }

    }
}
