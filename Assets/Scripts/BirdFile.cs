using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BirdFile : MonoBehaviour
{
    [SerializeField] private SuspectManager suspectManager;
    [SerializeField] private GameObject imageFrame;
    [SerializeField] private TextMeshProUGUI suspectName;
    [SerializeField] private TextMeshProUGUI suspectSpecies;
    [SerializeField] private TextMeshProUGUI suspectHeight;
    [SerializeField] private TextMeshProUGUI suspectDescription;
    [SerializeField] private TextMeshProUGUI suspectHabits;
    [SerializeField] private GameObject documentFiles;
    private Bird suspectBird;
    private Image photo;
    private 

    // Start is called before the first frame update
    void Start()
    {
        photo = imageFrame.GetComponent<Image>();
    }
    private void OnEnable()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
        
    public void QuitDocument()
    {
        documentFiles.SetActive(false);
    }
    public void UpdateDocument()
    {
        string habits = "";

        suspectBird = suspectManager.SelectedBird;

        photo.sprite = suspectBird.sprite;
        suspectName.text = suspectBird.name;
        suspectSpecies.text = suspectBird.species;
        suspectHeight.text = $"Height: {suspectBird.height}cm";
        suspectDescription.text = suspectBird.description;

        foreach (string habit in suspectBird.habits)
        {
            habits += $"{habit} ;\n";
        }
        suspectHabits.text = habits;
    }

    
}
