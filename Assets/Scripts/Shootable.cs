using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

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
    public bool Caught { get; private set; }

    private RandomPositionNPC rpnpc;

    private ScareBirds tempScare;

    private Collider col;
    private bool beingSeen;

    public Sprite caughtPicture;

    private void Start()
    {
        wfs = new WaitForSeconds(timeToExplode);
        wfsBadGuy = new WaitForSeconds(durationOfBadGuy);

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();


        BadDog = false;
        Caught = false;

        tempScare = null;
        tempScare = GetComponent<ScareBirds>();

        badPathing = false;

        if (skinnedMeshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        rpnpc = GetComponent<RandomPositionNPC>();
        //Debug.Log(rpnpc);
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (rpnpc != null)
        {
            badPathing = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * -1, Mathf.Infinity, badLayer);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * -10, Color.red);
            Debug.Log(badPathing);
        }

        beingSeen = CheckIfOnCamera();

        if (beingSeen)
        {
            Shooting.seenObjects.Add(gameObject);
        }
        else
        {
            if(Shooting.seenObjects.Contains(gameObject))
            {
                Shooting.seenObjects.Remove(gameObject);
            }
        }
    }

    public void StartExplosion()
    {
        StatsManager.instance.timesHit++;
        Instantiate(conffeti, transform.position, Quaternion.identity);

        if (BadGuy || badPathing || BadDog)
        {
            Debug.Log(BadGuy);
            Debug.Log(badPathing);
            Debug.Log(BadDog);

            if (BadDog) StatsManager.instance.dogsStopped++;
            if(badPathing || BadGuy) StatsManager.instance.trespassersKilled++;

            if (BadGuy && Caught)
            {
                

                Shooting.instance.caughtPictureUI.GetComponent<Image>().sprite = caughtPicture;
                Shooting.instance.TriggerCaughtUI();
                ScoreManager.instance.AddScore(20);
            }
            else
                ScoreManager.instance.AddScore(10);
            StopCoroutine(BadGuyForXSeconds());
        }
        else
        {
            BirdLogic bl = GetComponent<BirdLogic>();
            ScareBirds sb = GetComponent<ScareBirds>();

            if (bl != null) { StatsManager.instance.birdsKilled++; }
            else StatsManager.instance.touristKilled++;

            ScoreManager.instance.AddScore(-10);
        }

        Invoke(nameof(DestroyMe), 0.1f);
    }

    private void DestroyMe() => Destroy(gameObject);

    
    public void TurnBadGuy()
    {
        StartCoroutine(BadGuyForXSeconds());
    }

    public void CaughtDoingBadThings()
    {
        if(BadGuy)
        {
            Caught = true;
        }
    }

    private bool CheckIfOnCamera()
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), col.bounds);

    }

    private IEnumerator BadGuyForXSeconds()
    {

        BadGuy = true;

        yield return wfsBadGuy;

        if(Caught)
            ScoreManager.instance.AddScore(-3);
        else
            ScoreManager.instance.AddScore(-5);

        Caught = false;
        BadGuy = false;
    }

}
