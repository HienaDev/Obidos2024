using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RandomPositionNPC : MonoBehaviour
{

    [SerializeField] private float distance;
    [SerializeField] private float distanceToRetry;

    private NavMeshAgent agent;
    private Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()    
    {
        agent = GetComponent<NavMeshAgent>();

        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, nextPosition) < distanceToRetry) 
        { 
            RandomPosition();   
        }
    }

    public void RandomPosition()
    {
        agent.destination = RandomNavSphere();
        nextPosition = agent.destination;
    }

    private Vector3 RandomNavSphere()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);

        return navHit.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, distance);


        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, distanceToRetry);
        
    }
}
