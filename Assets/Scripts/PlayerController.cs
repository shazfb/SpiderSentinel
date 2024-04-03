using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Transform[] legs;
    public Transform body; 
    public float rotationSpeed = 180f; 

    private Vector3 movementDirection = Vector3.zero;
    private bool isMoving = false;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movementDirection.magnitude >= 0.1f)
        {
            RotateTowardsMovementDirection();
            MoveSpider();
        }
        else if (isMoving)
        {
            StopLegMovement();
        }
    }

    private void RotateTowardsMovementDirection()
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            body.rotation = Quaternion.RotateTowards(body.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void MoveSpider()
    {
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        StartLegMovement();
    }

    private void StartLegMovement()
    {
        foreach (Transform leg in legs)
        {
            BoneConstraintController legController = leg.GetComponent<BoneConstraintController>();
            if (legController != null && !legController.CheckIsMoving())
            {
                legController.StartCoroutine(legController.MoveLegOverTime());
            }
        }
        isMoving = true;
    }

    private void StopLegMovement()
    {
        foreach (Transform leg in legs)
        {
            BoneConstraintController legController = leg.GetComponent<BoneConstraintController>();
            if (legController != null && legController.CheckIsMoving())
            {
                legController.StopAllCoroutines();
            }
        }
        isMoving = false;
    }
}
