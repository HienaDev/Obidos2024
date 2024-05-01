using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private GameObject caughtUI;

    [SerializeField] private GameObject newSpeciesUI;
    [SerializeField] private GameObject iSawThatUI;
    public GameObject caughtPictureUI;

    private List<GameObject> uiTools;

    private SniperZoom zoomScript;

    public static HashSet<GameObject> seenObjects;
    public static List<string> seenSpecies;

    private PlaySoundsPlayer sounds;

    private Sprite lastPicture;

    public static Shooting instance;

    
    private void Awake()
    {
        instance = this;

        seenObjects = new HashSet<GameObject>();
        seenSpecies = new List<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        wfs = new WaitForSeconds(timerUI);

        rtc = GetComponent<RenderTextureCapture>();

        uiTools = new List<GameObject>();
        uiTools.Add(cameraUI);
        uiTools.Add(sniperUI);

        sounds = GetComponent<PlaySoundsPlayer>();

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
            {
                string type = CameraShooting();
                sounds.PlaySoundCam();
                
            }

            if (sniperUI.activeSelf)
            { 
                Shoot();
                sounds.PlaySoundShoot();
                StatsManager.instance.timeShot++;
            }
        }

        if(Input.GetKeyDown(KeyCode.E)) 
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                StartDay sd = hit.collider.gameObject.GetComponent<StartDay>();
                if (sd != null) 
                {
                    sd.ActivateStuff();
                }
            }
        }
    }


    private void ResetUI()
    {
        foreach (GameObject go in uiTools)
        {
            go.SetActive(false);
        }
    }

    public void TriggerCaughtUI()
    {
        StartCoroutine(ActivateForXSeconds(caughtPictureUI));
    }

    private string CameraShooting()
    {

        foreach(GameObject go in seenObjects)
        {
            if(go != null)
            {
                

                BirdLogic birdLogic = go.GetComponent<BirdLogic>();
                Shootable shootable = go.GetComponent<Shootable>();
                if (birdLogic != null)
                {
                    if (!seenSpecies.Contains(birdLogic.Species))
                    {
                        StatsManager.instance.speciesDiscovered++;
                        ScoreManager.instance.AddScore(10);
                        seenSpecies.Add(birdLogic.Species);
                        StartCoroutine(ActivateForXSeconds(newSpeciesUI));
                        StatsManager.instance.newSpecies.Add( rtc.ExportPhoto("new_species_"));
                    }
                    
                }
                else if (shootable != null)
                {
                    shootable.CaughtDoingBadThings();
                    if(shootable.Caught)
                    {
                        StatsManager.instance.touristsCaught++;
                        StartCoroutine(ActivateForXSeconds(iSawThatUI));
                        ScoreManager.instance.AddScore(5);
                        shootable.caughtPicture = rtc.ExportPhoto("caught_");

                        StatsManager.instance.caught.Add(shootable.caughtPicture);


                    }
                }

            }
            
        }
        StatsManager.instance.everythingElse.Add(rtc.ExportPhoto(""));
        return "";
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
                { 
                    if(temp.BadGuy && temp.Caught)
                        StartCoroutine(ActivateForXSeconds(caughtUI));

                    Invoke(nameof(ActivateGoodUI), 0.02f);
                }
                else
                    StartCoroutine(ActivateForXSeconds(badUI));

                temp.StartExplosion();
            }
        }
    }

    private void ActivateGoodUI() => StartCoroutine(ActivateForXSeconds(goodUI));

    private IEnumerator ActivateForXSeconds(GameObject ui)
    {

        ui.SetActive(true);
        yield return wfs;
        ui.SetActive(false);
    }

    public void ResetShooting()
    {
        seenObjects = new HashSet<GameObject>();
        seenSpecies = new List<string>();
    }
}
