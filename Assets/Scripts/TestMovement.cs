using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Material idleMaterial;
    [SerializeField] Material activeMaterial;

    public float activationRadius = 3;
    private float distanceToPlayer;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetComponent<MeshRenderer>().material = idleMaterial;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= activationRadius)
        {
            GetComponent<MeshRenderer>().material = activeMaterial;
            agent.destination = player.transform.position;
        }
        else
        {
            GetComponent<MeshRenderer>().material = idleMaterial;
            agent.destination = player.transform.position;
        }
    }
}
