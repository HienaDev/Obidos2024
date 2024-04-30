using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrimeFile : MonoBehaviour
{
    [SerializeField] private SuspectManager suspectManager;
    [SerializeField] private TextMeshProUGUI crimeName;

    // Start is called before the first frame update
    void Start()
    {
        crimeName.text = suspectManager.SelectedCrime;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void quitDocument()
    {
        gameObject.SetActive(false);
    }
}
