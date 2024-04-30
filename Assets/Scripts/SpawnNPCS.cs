using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCS : MonoBehaviour
{

    [SerializeField] private GameObject[] NPCs;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(NPCs[Random.Range(0, NPCs.Length)], transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
