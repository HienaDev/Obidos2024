using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCS : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] NPCs;
    [SerializeField] private Vector2 timer;
    private float cooldown;
    private float justSpawned;

    // Start is called before the first frame update
    void Start()
    {
        justSpawned = Time.time;
        cooldown = Random.Range(timer.x, timer.y);
        Debug.Log(cooldown);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - justSpawned > cooldown)
        {
            Instantiate(NPCs[Random.Range(0, NPCs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
            justSpawned = Time.time;
            cooldown = Random.Range(timer.x, timer.y);
            Debug.Log(cooldown);
        }
    }
}
