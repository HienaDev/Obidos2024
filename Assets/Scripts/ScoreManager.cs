using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    private int score;

    private TextMeshProUGUI textMeshProUGUI;

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
        textMeshProUGUI.text = score.ToString();
    }
}
