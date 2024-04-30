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

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        wfs = new WaitForSeconds(timerUI);

        rtc = GetComponent<RenderTextureCapture>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            rtc.ExportPhoto();
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
                if (temp.BadGuy == true)
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
