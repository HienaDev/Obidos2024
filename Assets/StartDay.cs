using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDay : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToStart;
    [SerializeField] private float timer = 240;

    public void ActivateStuff()
    {
        foreach (GameObject obj in objectsToStart)
        {
            obj.SetActive(true);
        }
        TimerUI.instance.StartTimer(timer);
        GetComponent<Animator>().SetTrigger("Shrink");
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
