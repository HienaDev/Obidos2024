using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public static TimerUI instance; 
    private float timer;
    private float minutes;
    private float seconds;
    private bool running;
    private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject birds;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        running = false;

        timer = -1;

        textMeshProUGUI = GetComponent<TextMeshProUGUI>();  
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            timer = 10;
        }

        if(running && timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(timer, 0);
            Debug.Log(minutes);
            minutes = (int)timer / 60;
            seconds = (int)timer % 60;
            textMeshProUGUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }

        if(timer == 0)
        { 
            StatsManager.instance.ActivateStats();
            running = false;
            birds.SetActive(false);
            SpawnNPCS.instance.ClearNPCs();
        }
    }

    public void StartTimer(float timer)
    {
        this.timer = timer; 
        running = true;
    }
}
