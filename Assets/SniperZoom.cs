using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperZoom : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float zoomScale;
    private float defaultZoom;
    private float currentZoom;

    [SerializeField] private float howFastZoom;
    private float lerpValue;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        defaultZoom = cam.fieldOfView;
        currentZoom = cam.fieldOfView;

        lerpValue = 1;

        Debug.Log(zoomScale);
        Debug.Log(defaultZoom);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1)) lerpValue = 0;
        if (Input.GetMouseButtonUp(1)) lerpValue = 0;

        if (Input.GetMouseButton(1)) ZoomIn();
        else ZoomOut();

        //Debug.Log(lerpValue);
    }

    private void ZoomIn()
    {

        if (lerpValue < 1)
        {
            lerpValue += howFastZoom * Time.deltaTime;
            currentZoom = Mathf.Lerp(defaultZoom, zoomScale, lerpValue);
            cam.fieldOfView = currentZoom;
        }

        
    }

    private void ZoomOut()
    {
        if (lerpValue < 1)
        {
            lerpValue += howFastZoom * Time.deltaTime;
            currentZoom = Mathf.Lerp(zoomScale, defaultZoom, lerpValue);
            cam.fieldOfView = currentZoom;
        }
    }
}