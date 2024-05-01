using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public int score;

    private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject lostScore;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        score = 0;

        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }



    public void AddScore(int value)
    {
        score += value;

        StartCoroutine(ActivateForXSeconds());


        if (value > 0)
        {
            lostScore.GetComponentInChildren<TextMeshProUGUI>().text = "+" + value + "$";
        }
        else
            lostScore.GetComponentInChildren<TextMeshProUGUI>().text = value + "$";

        
        textMeshProUGUI.text = score.ToString();
    }

    private IEnumerator ActivateForXSeconds()
    {

        lostScore.SetActive(true);
        yield return new WaitForSeconds(5);
        lostScore.SetActive(false);
    }
}
