using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BirdFile : MonoBehaviour
{
    [SerializeField] private SuspectManager suspectManager;
    [SerializeField] private GameObject imageFrame;
    [SerializeField] private TextMeshProUGUI suspectName;
    private Bird suspectBird;
    private Image photo;

    // Start is called before the first frame update
    void Start()
    {
        photo = imageFrame.GetComponent<Image>();
    }
    private void OnEnable()
    {
        suspectBird = suspectManager.SelectedBird;
    }

    // Update is called once per frame
    void Update()
    {
        photo.sprite = suspectBird.sprite;
        suspectName.text = suspectBird.name;
    }
    public void quitDocument()
    {
        gameObject.SetActive(false);
    }
}
