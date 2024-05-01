using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    private bool[] occupied;

    [SerializeField] public Transform scarePoint;
    public static BirdManager instance; 


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
}
