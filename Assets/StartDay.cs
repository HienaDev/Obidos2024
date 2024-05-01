using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDay : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToStart;


    public void ActivateStuff()
    {
        foreach (GameObject obj in objectsToStart)
        {
            obj.SetActive(true);
        }
        GetComponent<Animator>().SetTrigger("Shrink");
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
