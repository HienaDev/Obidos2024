using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    [SerializeField] private float timerThrowTrash = 5;
    [SerializeField, Range(0, 100)] private int chanceThrowTrash = 100;
    private float justThrewTrash;

    [SerializeField] private GameObject[] trash;

    // Start is called before the first frame update
    void Start()
    {
        justThrewTrash = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (Time.time - justThrewTrash > timerThrowTrash) 
        { 
            ThrowTrash();
        }
    }

    private void ThrowTrash()
    {
        justThrewTrash = Time.time;

        if (Random.Range(0, 100) < chanceThrowTrash)
        {
            Instantiate(trash[Random.Range(0, trash.Length)], transform.position, Quaternion.identity);
            GetComponent<Shootable>().TurnBadGuy();
        }
    }
}
