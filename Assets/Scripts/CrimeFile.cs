using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrimeFile : MonoBehaviour
{
    [SerializeField] private SuspectManager suspectManager;
    [SerializeField] private TextMeshProUGUI crimeName;
    [SerializeField] private TextMeshProUGUI crimeDescription;
    [SerializeField] private GameObject documentFiles;

    // Start is called before the first frame update
    void Start()
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
        crimeName.text = suspectManager.SelectedCrime;
        crimeDescription.text = suspectManager.CrimeDescription;
    }
}
