using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> pointsList;
    [SerializeField] int minChooseTime;
    [SerializeField] int maxChooseTime;
    // [SerializeField] int minWaitTime;
    // [SerializeField] int maxWaitTime;
    [SerializeField] float speed;
    private static bool[] boolList;
    private Transform pointTrans;
    private bool isInPos = false;
    private float accelaration = 0;
    private Transform startPos;
    

    // Start is called before the first frame update
    void Start()
    {
        boolList = new bool[pointsList.Count];
        for (int i = 0; i < boolList.Length; i++)
        {
            boolList[i] = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isInPos)
        {
            StartCoroutine(ChooseRandomPos());
        }

        if (accelaration < 1)
        {
            accelaration += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos.position,pointTrans.position,accelaration);
        }
        
        
    }
    private IEnumerator ChooseRandomPos()
    {
        isInPos = true;
        
        int lenList = pointsList.Count;
        int randList = Random.Range(0,lenList-1);
        if (!boolList[randList])
        {
            ChooseRandomPos();
        }
        
        Debug.Log(boolList[randList]);
        
        boolList[randList] = false;
        
        Debug.Log("START");
        int chooseTime = Random.Range(minChooseTime,maxChooseTime+1);
        Debug.Log($"ChooseTime : {chooseTime}");
        yield return new WaitForSeconds(chooseTime);
        Debug.Log("END");

        accelaration = 0;

        Debug.Log(pointsList[randList]);
        Debug.Log(boolList[randList]);

        startPos = transform;
        pointTrans = pointsList[randList].GetComponent<Transform>();
        
        isInPos = false;
        // StartCoroutine(WaitInPoint(randList));
    }
    // private IEnumerator WaitInPoint(int inPoint)
    // {
    //     Debug.Log("START");
    //     int waitTime = Random.Range(minWaitTime,maxWaitTime+1);
    //     Debug.Log($"WaitTime : {waitTime}");
    //     yield return new WaitForSeconds(waitTime);
    //     boolList[inPoint] = true;
    //     Debug.Log("END");
    //     transform.position = new Vector3(0f,0f,0f);
    //     StartCoroutine(ChooseRandomPos());
    // }
}
