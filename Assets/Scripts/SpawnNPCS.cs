using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnNPCS : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] NPCs;
    [SerializeField] private Vector2 timer;
    private float cooldown;
    private float justSpawned;

    [SerializeField] private GameObject npcHolder;

    private List<GameObject> spawned;

    public static SpawnNPCS instance;


    private void Awake()
    {
        instance = this;
    }

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
            NavMeshHit closestHit;
            NavMesh.SamplePosition(spawnPoints[Random.Range(0, spawnPoints.Length)].position, out closestHit, 500, -1);
            //spawned.Add(Instantiate(NPCs[Random.Range(0, NPCs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]));
            Instantiate(NPCs[Random.Range(0, NPCs.Length)], closestHit.position, Quaternion.identity, npcHolder.transform);
            //spawned.Add(npc);
            justSpawned = Time.time;
            cooldown = Random.Range(timer.x, timer.y);
            Debug.Log(cooldown);
        }
    }


    public void ClearNPCs()
    {
        foreach (Transform child in npcHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResetNPCS()
    {
        

        justSpawned = Time.time;
        cooldown = Random.Range(timer.x, timer.y);
        Debug.Log("reset");
    }
}
