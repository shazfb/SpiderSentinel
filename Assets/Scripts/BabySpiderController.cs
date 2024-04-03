using UnityEngine;

public class BabySpiderController : MonoBehaviour
{
    public Transform leader; 
    public Transform followPoint; 
    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float stoppingDistance = 0.1f; 

    private bool isMoving = true; 

    void Update()
    {
        if (leader != null && followPoint != null)
        {
            Vector3 direction = followPoint.position - transform.position;

            float distanceToFollowPoint = direction.magnitude;

            if (isMoving || distanceToFollowPoint > stoppingDistance)
            {
                transform.position += direction.normalized * speed * Time.deltaTime;

                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if (distanceToFollowPoint <= stoppingDistance)
                {
                    isMoving = false;
                }
            }
            else
            {
                if (Vector3.Distance(followPoint.position, transform.position) > stoppingDistance)
                {
                    isMoving = true;
                }
            }
        }
    }
}
