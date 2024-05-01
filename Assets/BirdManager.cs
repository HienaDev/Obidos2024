using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    private bool[] occupied;

    [SerializeField] public Transform scarePoint;
    public static BirdManager instance;

    [SerializeField] private Transform[] birds;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        occupied = new bool[waypoints.Length];
    }



    public Transform GetWaypoint()
    {
        bool newSlot = true;
        int slot = 0;

        while (newSlot == true)
        {
            slot = Random.Range(0, waypoints.Length);
            newSlot = occupied[slot];
        }

        occupied[slot] = true;
        //Debug.Log(slot + occupied[slot].ToString());

        return waypoints[slot];
    }

    public void LeaveWaypoints(Transform waypoint)
    {

        occupied[System.Array.IndexOf(waypoints, waypoint)] = false;
    }

    public void ResetBirds()
    {
        for (int i = 0; i < occupied.Length; i++) { occupied[i] = false; }

        for (int i = 0;i < birds.Length;i++) 
        {
            int slot = Random.Range(0, waypoints.Length);

            if (!occupied[slot]) 
            {
                birds[i].position = waypoints[slot].position;
                occupied[slot] = true;
            }
            
        }
    }
}
