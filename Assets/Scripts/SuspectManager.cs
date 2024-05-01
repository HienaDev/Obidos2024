using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuspectManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> mugshotList;
    [SerializeField] private List<Bird> allBirdsList;
    [SerializeField] private TextMeshProUGUI debugCriminal;
    [SerializeField] private TextMeshProUGUI debugSelect;
    [SerializeField] private TextMeshProUGUI debugWin;
    [SerializeField] private Animator birdFileAnim;
    [SerializeField] private GameObject birdFileScene;
    [SerializeField] private GameObject crimeFileScene;
    [SerializeField] private GameObject veredictScene;
    [SerializeField] private GameObject veredictPhoto;
    [SerializeField] private GameObject selectImage;
    [SerializeField] private Button guiltyButton;
    private Bird[] birdList = new Bird[5];
    private Image buttonImage;
    public Bird SelectedBird { get; private set; }
    private GameObject selectedButton = null;
    private Bird guiltyBird;
    public string SelectedCrime { get; private set; }
    public string CrimeDescription { get; private set; }
    private bool[] select = new bool[5] {false, false, false, false, false};

    // Start is called before the first frame update
    void Start()
    {
        ChooseBirds();

        int intBird = 0;
        foreach (GameObject mug in mugshotList)
        {
            buttonImage = mug.GetComponent<Image>();
            mug.GetComponentInChildren<TextMeshProUGUI>().text = birdList[intBird].name;
            buttonImage.sprite = birdList[intBird].sprite;

            intBird++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{select[0]}, {select[1]}, {select[2]}, {select[3]}, {select[4]}");
        if (SelectedBird != null)
        {
            guiltyButton.enabled = true;
        }
        else
        {
            guiltyButton.enabled = false;
        }

        UpdateSelectPosition();
    }
    public void ChooseBirds()
    {
        for (int i = 0; i < 5; i++)
        {
            ChooseRandomBird(i);
        }
        
        guiltyBird = birdList[Random.Range(0,4)];
        debugCriminal.text = $"The culprate is {guiltyBird.name}";
        ChooseCrime();
    }
    private void ChooseCrime()
    {
        int randInt = Random.Range(0,guiltyBird.crimes.Length);
        SelectedCrime = guiltyBird.crimes[randInt];
        CrimeDescription = guiltyBird.crimesDescription[randInt];
    }
    private void ChooseRandomBird(int listInd)
    {
        int randInt = Random.Range(0,allBirdsList.Count);

        if (allBirdsList[randInt] != null)
        {
            birdList[listInd] = allBirdsList[randInt];
            allBirdsList[randInt] = null;
        }
        else
        {
            ChooseRandomBird(listInd);
        }
        
    }
    private void ResetSelectExceptOne(int num)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == num)
            {
                select[i] = true;
            }
            else
            {
                select[i] = false;
            }
        }
    }
    private bool CheckIfOtherTrue()
    {
        bool result = false;
        foreach (bool sel in select)
        {
            if (sel)
            {
                result = true;
            }
        }
        return result;
    }
    public void SelectBird1()
    {
        if (!select[0])
        {
            if (CheckIfOtherTrue()) 
                birdFileAnim.SetTrigger("Hide");
            birdFileAnim.SetTrigger("Show");
            SelectedBird = birdList[0];
            selectedButton = mugshotList[0];
            debugSelect.text = $"Selected: {SelectedBird.name}";
            ResetSelectExceptOne(0);
        }
        else
        {
            birdFileAnim.SetTrigger("Hide");
            SelectedBird = null;
            debugSelect.text = $"Selected: ";
            select[0] = false;
        }
        
    }
    public void SelectBird2()
    {
        if (!select[1])
        {
            if (CheckIfOtherTrue()) 
                birdFileAnim.SetTrigger("Hide");
            birdFileAnim.SetTrigger("Show");
            SelectedBird = birdList[1];
            selectedButton = mugshotList[1];
            debugSelect.text = $"Selected: {SelectedBird.name}";
            ResetSelectExceptOne(1);
        }
        else
        {
            birdFileAnim.SetTrigger("Hide");
            SelectedBird = null;
            debugSelect.text = $"Selected: ";
            select[1] = false;
        }
        
    }
    public void SelectBird3()
    {
        if (!select[2])
        {
            if (CheckIfOtherTrue()) 
                birdFileAnim.SetTrigger("Hide");
            birdFileAnim.SetTrigger("Show");
            SelectedBird = birdList[2];
            selectedButton = mugshotList[2];
            debugSelect.text = $"Selected: {SelectedBird.name}";
            ResetSelectExceptOne(2);
        }
        else
        {
            birdFileAnim.SetTrigger("Hide");
            SelectedBird = null;
            debugSelect.text = $"Selected: ";
            select[2] = false;
        }
        
    }
    public void SelectBird4()
    {
        if (!select[3])
        {
            if (CheckIfOtherTrue()) 
                birdFileAnim.SetTrigger("Hide");
            birdFileAnim.SetTrigger("Show");
            SelectedBird = birdList[3];
            selectedButton = mugshotList[3];
            debugSelect.text = $"Selected: {SelectedBird.name}";
            ResetSelectExceptOne(3);
        }
        else
        {
            birdFileAnim.SetTrigger("Hide");
            SelectedBird = null;
            debugSelect.text = $"Selected: ";
            select[3] = false;
        }
        
    }
    public void SelectBird5()
    {
        if (!select[4])
        {
            if (CheckIfOtherTrue()) 
                birdFileAnim.SetTrigger("Hide");
            birdFileAnim.SetTrigger("Show");
            SelectedBird = birdList[4];
            selectedButton = mugshotList[4];
            debugSelect.text = $"Selected: {SelectedBird.name}";
            ResetSelectExceptOne(4);
        }
        else
        {
            birdFileAnim.SetTrigger("Hide");
            SelectedBird = null;
            debugSelect.text = $"Selected: ";
            select[4] = false;
        }
        
    }
    public void OpenBirdFile()
    {
        birdFileScene.SetActive(true);
    }
    public void OpenCrimeFile()
    {
        crimeFileScene.SetActive(true);
    }
    public void AccuseBird()
    {
        if (SelectedBird == guiltyBird)
        {
            debugWin.text = "YOU WERE RIGHT!";
        }
        else 
        {
            debugWin.text = "YOU WERE WRONG.";
        }
        veredictScene.SetActive(true);
        veredictPhoto.GetComponent<Image>().sprite = SelectedBird.sprite;
    }
    private void UpdateSelectPosition()
    {
        if (CheckIfOtherTrue())
        {
            selectImage.SetActive(true);
            selectImage.transform.position = selectedButton.transform.position;
        }
        else
        {
            selectImage.SetActive(false);
        }
        
    }
}
