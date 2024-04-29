using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomVelocityRB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3 (Random.Range(-4f, 4f), Random.Range(2f, 4f), Random.Range(-4f, 4f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
