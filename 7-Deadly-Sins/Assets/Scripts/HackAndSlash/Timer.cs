using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    public bool TimerStart;
    HackAndSlashManager manager;
    public float finalTime;
    public bool finalTimeUpdated;
    
    

    private void Start()
    {
        //Set time when the first person start the game
        startTime = 0;
        TimerStart = false;
        manager = HackAndSlashManager.instance;
    }

    private void Update()
    {
        if (!manager.allLevelDone)
        {
            if (TimerStart)
            {
                startTime += Time.deltaTime * 1f;
                string minutes = ((int)startTime / 60).ToString();
                string seconds = (startTime % 60).ToString("f2");
                timerText.text = minutes + ":" + seconds;
            }
        }else
        {
            finalTime = startTime;
            finalTimeUpdated = true;
            StopTime();
            timerText.color = Color.red;
            
        }

       
    }

    public void StopTime()
    {
        TimerStart = false;
        
    }

    public void StartTime()
    {
        TimerStart = true;
    }
}
