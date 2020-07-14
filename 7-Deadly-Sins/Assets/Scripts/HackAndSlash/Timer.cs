using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Canvas timerCanvas;
    public TextMeshProUGUI timerText;
    private float startTime;
    public bool TimerStart;
    HackAndSlashManager manager;
    public float finalTime;
    public bool finalTimeUpdated;
    public LiftMovement liftMovement;
    private bool timerto2;
    private bool timerto3;
    RectTransform timerTransform;
    
    

    private void Start()
    {
        //Set time when the first person start the game
        startTime = 0;
        TimerStart = false;
        manager = HackAndSlashManager.instance;
        timerTransform = timerCanvas.transform.GetComponent<RectTransform>();
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

        if (liftMovement.reachedLevel2 && !timerto2 )
        {
            timerto2 = true;
            timerTransform.anchoredPosition = new Vector3(timerTransform.anchoredPosition.x, 2.06f, timerTransform.position.z);
        }else if (liftMovement.reachedLevel3 && !timerto3)
        {
            timerto3 = true;
            timerTransform.anchoredPosition = new Vector3(timerTransform.anchoredPosition.x, 5.63f, timerTransform.position.z);
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
