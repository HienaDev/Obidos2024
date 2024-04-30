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
        // StartCoroutine(WaitTimer());
    }

    // Update is called once per frame
    void Update()
    {
        

        // if (accelaration < 1)
        // {
        //     
        //     if (transform.position == pointTrans.position)
        //     {
        //         isInPos = false;
        //     }
        //     if (!isInPos)
        //     {
        //         StartCoroutine(ChooseRandomPos());
        //     }
        // }
        int randList = ChooseListInt();
        boolList[randList] = false;

        startPos = transform;
        pointTrans = pointsList[randList].GetComponent<Transform>();
        DoBirdMovement();

        if (transform.position == pointTrans.position)
        {
            StartCoroutine(WaitTimer());
        }
    }
    private IEnumerator WaitTimer()
    {
        isInPos = true;
        
        int chooseTime = Random.Range(minChooseTime,maxChooseTime+1);
        yield return new WaitForSeconds(chooseTime);
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
    private int ChooseListInt()
    {
        int result;
        int lenList = pointsList.Count - 1;

        result = Random.Range(0,lenList);
        Debug.Log(result);
        if (!boolList[result])
        {
            Debug.Log("AAAAAAAAA");
            result = ChooseListInt();
        }
        return result;
    }
    private void DoBirdMovement()
    {
        accelaration += speed * Time.deltaTime;
        transform.position = Vector3.Lerp(startPos.position,pointTrans.position,accelaration);
    }
}
