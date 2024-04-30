using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private float timeToExplode = 5;
    private YieldInstruction wfs;

    [SerializeField] private GameObject conffeti;
    [SerializeField] private float durationOfBadGuy = 10;
    private YieldInstruction wfsBadGuy;


    [SerializeField] private LayerMask badLayer;
    public bool badPathing;

    public bool BadDog {  get; set; }

    public bool BadGuy {  get; private set; }

    private RandomPositionNPC rpnpc;

    private ScareBirds tempScare;



    private void Start()
    {
        wfs = new WaitForSeconds(timeToExplode);
        wfsBadGuy = new WaitForSeconds(durationOfBadGuy);

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();


        BadDog = false;

        tempScare = null;
        tempScare = GetComponent<ScareBirds>();

        badPathing = false;

        if (skinnedMeshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        rpnpc = GetComponent<RandomPositionNPC>();
        //Debug.Log(rpnpc);
    }

    private void FixedUpdate()
    {
        if (rpnpc != null)
        {
            badPathing = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * -1, Mathf.Infinity, badLayer);
            //Debug.Log(badPathing);
        }
            

        
    }

    public void StartExplosion()
    {
        Instantiate(conffeti, transform.position, Quaternion.identity);

        if (BadGuy || badPathing || BadDog)
        {
            Debug.Log(BadGuy);
            Debug.Log(badPathing);
            Debug.Log(BadDog);
            ScoreManager.instance.AddScore(10);
            StopCoroutine(BadGuyForXSeconds());
        }
        else
        {
            ScoreManager.instance.AddScore(-10);
        }

        Invoke(nameof(DestroyMe), 0.1f);
    }

    private void DestroyMe() => Destroy(gameObject);

    
    public void TurnBadGuy()
    {
        StartCoroutine(BadGuyForXSeconds());
    }

    private IEnumerator BadGuyForXSeconds()
    {

        BadGuy = true;
        yield return wfsBadGuy;
        ScoreManager.instance.AddScore(-1);
        BadGuy = false;
    }

}
