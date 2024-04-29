using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private float timeToExplode = 5;
    private YieldInstruction wfs;

    [SerializeField] private GameObject conffeti;

    // Start is called before the first frame update
    void Start()
    {

        wfs = new WaitForSeconds(timeToExplode);

        meshRenderer = GetComponentInChildren<MeshRenderer>();

        Debug.Log(meshRenderer);

        if (meshRenderer == null)
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();


        Debug.Log(skinnedMeshRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartExplosion()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
        else
        {
            skinnedMeshRenderer.enabled = false;
        }

        Instantiate(conffeti, transform);
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(timeToExplode);

        Destroy(gameObject);
    }

}
