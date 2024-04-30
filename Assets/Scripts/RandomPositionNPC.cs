using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RandomPositionNPC : MonoBehaviour
{

    [SerializeField] private float distance = 40;
    [SerializeField] private float distanceToRetry = 3;

    [SerializeField, Range(0, 100)] private int chanceForBad;

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

        

        if(agent.agentTypeID == GetAgenTypeIDByName("Humanoid Bad"))
        {
            agent.agentTypeID = GetAgenTypeIDByName("Humanoid");
        }
        else if (Random.Range(0, 100) < chanceForBad)
        {
            agent.agentTypeID = GetAgenTypeIDByName("Humanoid Bad");
        }

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

    private int GetAgenTypeIDByName(string agentTypeName)
    {
        int count = NavMesh.GetSettingsCount();
        string[] agentTypeNames = new string[count + 2];
        for (var i = 0; i < count; i++)
        {
            int id = NavMesh.GetSettingsByIndex(i).agentTypeID;
            string name = NavMesh.GetSettingsNameFromID(id);
            if (name == agentTypeName)
            {
                return id;
            }
        }
        return -1;
    }

    public bool IsBadHumanoid() => agent.agentTypeID == GetAgenTypeIDByName("Humanoid Bad");
}
