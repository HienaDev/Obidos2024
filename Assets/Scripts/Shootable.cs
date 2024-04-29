using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private ScoreManager scoreManager;

    


    public bool BadGuy {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        wfs = new WaitForSeconds(timeToExplode);
        wfsBadGuy = new WaitForSeconds (durationOfBadGuy);

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        scoreManager = ScoreManager.instance;

        //Debug.Log(meshRenderer);

        if (skinnedMeshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();


        //Debug.Log(skinnedMeshRenderer);
    }

    private void FixedUpdate()
    {
        //Debug.Log(gameObject.name +  BadGuy);  
    }

    public void StartExplosion()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.enabled = false;
            
        }
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        Instantiate(conffeti, transform);
        GetComponent<Collider>().enabled = false;

        if (BadGuy)
        {
            scoreManager.AddScore(10);
            StopCoroutine(BadGuyForXSeconds());
        }
        else scoreManager.AddScore(-10);

        yield return wfs;

        Destroy(gameObject);
    }

    public void TurnBadGuy()
    {
        StartCoroutine(BadGuyForXSeconds());
    }

    private IEnumerator BadGuyForXSeconds()
    {

        BadGuy = true;
        yield return wfsBadGuy;
        scoreManager.AddScore(-1);
        BadGuy = false;
    }

}
