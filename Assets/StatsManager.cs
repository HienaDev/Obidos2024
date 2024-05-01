using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{

    public static StatsManager instance;

    [SerializeField] private PlayerMovement playerScript;

    public GameObject finalStats;

    public List<Sprite> newSpecies = new List<Sprite>();
    public List<Sprite> caught = new List<Sprite>();
    public List<Sprite> everythingElse = new List<Sprite>();

    [SerializeField] private GameObject photosSpecies;
    private Image speciesImage;
    private int speciesIndex = 0;
    [SerializeField] private GameObject caughtPhotos;
    private Image caughtImage;
    private int caughtIndex = 0;
    [SerializeField] private GameObject allTheOthers;
    private Image othersImage;
    private int othersIndex = 0;


    private int day = 1;

    public int timeShot;
    public int timesHit;
    public int touristKilled;
    public int trespassersKilled;
    public int birdsKilled;
    public int speciesDiscovered;
    public int dogsStopped;
    public int touristsCaught;

    [SerializeField] private Material day1;
    [SerializeField] private Material day2;
    [SerializeField] private Material day3;

    [SerializeField] private SpawnNPCS spawner;

    [SerializeField] private GameObject birds;

    public int PhotosTaken
    {
        get
        {
            return newSpecies.Count + caught.Count + everythingElse.Count;
        }
    }

    public int PhotosNewSpecies
    {
        get
        {
            return newSpecies.Count;
        }
    }

    public int PhotosCaught
    {
        get
        {
            return caught.Count;
        }
    }


    [SerializeField] private TextMeshProUGUI timeShots;
    [SerializeField] private TextMeshProUGUI timesHits;
    [SerializeField] private TextMeshProUGUI touristKilleds;
    [SerializeField] private TextMeshProUGUI trespassersKilleds;
    [SerializeField] private TextMeshProUGUI birdsKilleds;
    [SerializeField] private TextMeshProUGUI speciesDiscovereds;
    [SerializeField] private TextMeshProUGUI dogsStoppeds;
    [SerializeField] private TextMeshProUGUI touristsCaughts;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private GameObject dLight;

    private int finalScore;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        speciesImage = photosSpecies.GetComponent<Image>();
        caughtImage = caughtPhotos.GetComponent<Image>();
        othersImage = allTheOthers.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateStats()
    {
        playerScript.DisableMovement();

        finalStats.SetActive(true);

        finalScore = ScoreManager.instance.score;

        timeShots.text = "Shots Fired: " + timeShot.ToString();
        timesHits.text = "Shots Hit: " + timesHit.ToString();
        touristKilleds.text = "Respecting Tourists Removed: " + touristKilled.ToString();
        trespassersKilleds.text = "Disrespecting Tourists Removed: " + trespassersKilled.ToString();
        birdsKilleds.text = "Birds Killed: " + birdsKilled.ToString() + " (WHY????)";
        speciesDiscovereds.text = "Species Catagoled: " + speciesDiscovered.ToString();
        dogsStoppeds.text = "Dogs Removed: " + dogsStopped.ToString();
        touristsCaughts.text = "Tourists Caught Disrespecting: " + touristsCaught.ToString();
        score.text = "Money Received: " + finalScore.ToString() + "$";

        if (newSpecies.Count > 0)
            speciesImage.sprite = newSpecies[speciesIndex];
        if (caught.Count > 0)
            caughtImage.sprite = caught[caughtIndex];
        if (everythingElse.Count > 0)
            othersImage.sprite = everythingElse[othersIndex];
    }

    public void LeftArrowSpecies()
    {
        if (newSpecies.Count > 0)
        {
            if (speciesIndex == 0) speciesIndex = newSpecies.Count - 1;
            else speciesIndex--;

            speciesImage.sprite = newSpecies[speciesIndex];
        }
    }

    public void RightArrowSpecies()
    {
        if (newSpecies.Count > 0)
        {
            if (speciesIndex == newSpecies.Count - 1) speciesIndex = 0;
            else speciesIndex++;

            speciesImage.sprite = newSpecies[speciesIndex];
        }
    }

    public void LeftArrowCaught()
    {
        if (caught.Count > 0)
        {
            if (caughtIndex == 0) caughtIndex = caught.Count - 1;
            else caughtIndex--;

            caughtImage.sprite = caught[caughtIndex];
        }
    }

    public void RightArrowCaught()
    {
        if (caught.Count > 0)
        {
            if (caughtIndex == caught.Count - 1) caughtIndex = 0;
            else caughtIndex++;

            caughtImage.sprite = caught[caughtIndex];
        }
    }

    public void LeftArrowOthers()
    {
        if (everythingElse.Count > 0)
        {
            if (othersIndex == 0) othersIndex = everythingElse.Count - 1;
            else othersIndex--;

            othersImage.sprite = everythingElse[othersIndex];
        }
    }

    public void RightArrowOthers()
    {
        if (everythingElse.Count > 0)
        {
            if (othersIndex == everythingElse.Count - 1) othersIndex = 0;
            else othersIndex++;

            othersImage.sprite = everythingElse[othersIndex];
        }
    }

    public void NextDay()
    {
        day++;
        playerScript.EnableMovement();
        TimerUI.instance.StartTimer(60);
        
        spawner.ResetNPCS();
        birds.SetActive(true);
        Shooting.instance.ResetShooting();
        finalScore = 0;

        BirdManager.instance.ResetBirds();

        if ( day % 3 == 2)
        {
            dLight.transform.eulerAngles = new Vector3(-5, dLight.transform.eulerAngles.y, dLight.transform.eulerAngles.z);
            dLight.GetComponent<Light>().colorTemperature = 1882;
            RenderSettings.skybox = day2;
        }
        else if (day % 3 == 0)
        {
            dLight.transform.eulerAngles = new Vector3(-90, dLight.transform.eulerAngles.y, dLight.transform.eulerAngles.z);
            dLight.GetComponent<Light>().colorTemperature = 20000;
            RenderSettings.skybox = day3;
        }
        else if (day % 3 == 1)
        {
            dLight.transform.eulerAngles = new Vector3(50, dLight.transform.eulerAngles.y, dLight.transform.eulerAngles.z);
            dLight.GetComponent<Light>().colorTemperature = 4000;
            RenderSettings.skybox = day1;
        }


        Invoke(nameof(DeactivateObject), 0.5f);
    }

    public void DeactivateObject() => finalStats.SetActive(false);
}
