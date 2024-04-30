using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{


    [SerializeField] private GameObject tempObject;
    private Camera cam;

    [SerializeField] private GameObject goodUI;
    [SerializeField] private GameObject badUI;
    [SerializeField] private float timerUI = 3;
    private YieldInstruction wfs;

    private RenderTextureCapture rtc;

    [SerializeField, Header("Tools")] private int cameraNumber = 1;
    [SerializeField] private GameObject cameraUI;

    [SerializeField] private int sniperNumber = 2;
    [SerializeField] private GameObject sniperUI;

    private List<GameObject> uiTools;

    private SniperZoom zoomScript;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        wfs = new WaitForSeconds(timerUI);

        rtc = GetComponent<RenderTextureCapture>();

        uiTools = new List<GameObject>();
        uiTools.Add(cameraUI);
        uiTools.Add(sniperUI);

        zoomScript = GetComponent<SniperZoom>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown((KeyCode)(48 + cameraNumber)))
        {
            ResetUI();
            zoomScript.SetCurrentCrosshair(cameraUI);

        }

        if (Input.GetKeyDown((KeyCode)(48 + sniperNumber)))
        {
            ResetUI();
            zoomScript.SetCurrentCrosshair(sniperUI);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(cameraUI.activeSelf)
                rtc.ExportPhoto();

            if (sniperUI.activeSelf)
                Shoot();

        }
    }

    private void ResetUI()
    {
        foreach (GameObject go in uiTools)
        {
            go.SetActive(false);
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            //Instantiate(tempObject, hit.point, Quaternion.identity);
            Shootable temp = hit.collider.gameObject.GetComponent<Shootable>();

            if (temp != null)
            {
                if (temp.BadGuy || temp.badPathing || temp.BadDog)
                    StartCoroutine(ActivateForXSeconds(goodUI));
                else
                    StartCoroutine(ActivateForXSeconds(badUI));

                temp.StartExplosion();
            }
        }
    }

    private IEnumerator ActivateForXSeconds(GameObject ui)
    {

        ui.SetActive(true);
        yield return wfs;
        ui.SetActive(false);
    }
}
