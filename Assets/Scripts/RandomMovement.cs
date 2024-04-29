using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> pointsList;
    [SerializeField] int minWaitTime;
    [SerializeField] int maxWaitTime;
    private bool[] boolList;
    

    // Start is called before the first frame update
    void Start()
    {
        boolList = new bool[pointsList.Count];
        for (int i = 0; i < boolList.Length; i++)
        {
            boolList[i] = true;
        }
        StartCoroutine(ChooseRandomPos());
    }

    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator ChooseRandomPos()
    {
        int lenList = pointsList.Count;
        int randList = Random.Range(0,lenList-1);
        if (!boolList[randList])
        {
            ChooseRandomPos();
        }
        
        Debug.Log("START");
        yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime+1));
        Debug.Log("END");
        Debug.Log(pointsList[randList]);
    }
}
