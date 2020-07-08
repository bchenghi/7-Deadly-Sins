using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class CheckSequence : MonoBehaviour
{
    [SerializeField]
    public Switch[] switches;
    public int[] correctSequence;
    private List<int> PlayerSwitchOrder;
    private bool restart;
    private bool checkSequence;
    private int switchCount;
    private int correctSwitches;
    public bool Success;
    private List<int> correctSwitchesCount;
   

    private void Start()
    {
        restart = true;
        checkSequence = false;
        switchCount = 0;
        PlayerSwitchOrder = new List<int>();
        Success = false;
        correctSwitches = 0;
        correctSwitchesCount = new List<int>();
    }

    private void Update()
    {
        if (restart && !checkSequence)
        {
            ClearPlayerSequenceList();
            CheckForAnySwitchActivation();
        }

        if (checkSequence)
        {
            //CheckList();
            GetPlayerSequence();
            CheckSequenceByCount();
            CheckSuccess();
        }

        if (Success)
        {
            //Open Door
        }

    }

    private void CheckForAnySwitchActivation()
    {
        if (!checkSequence)
        {
            for (int i = 0; i < switches.Length; i++)
            {
                if (switches[i].engaged)
                {
                    checkSequence = true;
                    break;
                }
            }
        }
    }

  

    private void CheckSequenceByCount()
    {
        if (switchCount > 0)
        {
            for (int i = 0; i < PlayerSwitchOrder.Count; i++)
            {
                if (PlayerSwitchOrder[i] != correctSequence[i])
                {
                    // As long as there is a wrong sequence it restarts
                    restart = true;
                    checkSequence = false;
                    switchCount = 0;
                    correctSwitches = 0;
                    correctSwitchesCount.Clear();
                    StartCoroutine(CheckcoolDown());
                    break;
                    
                } else
                {
                    if (!correctSwitchesCount.Contains(PlayerSwitchOrder[i]))
                    {
                        correctSwitchesCount.Add(PlayerSwitchOrder[i]);
                        correctSwitches++;
                    }
                }
            }
        }
    }

    private void GetPlayerSequence()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].engaged)
            {
                if (!PlayerSwitchOrder.Contains(switches[i].SwitchNumber))
                {
                    PlayerSwitchOrder.Add(switches[i].SwitchNumber);
                    switchCount++;
                    
                }
            }
        }
    }

    private void CheckList()
    {
       for (int i = 0; i < PlayerSwitchOrder.Count; i++)
        {
            Debug.Log(PlayerSwitchOrder[i]);
        }
    }

    private void ClearPlayerSequenceList()
    {
        PlayerSwitchOrder.Clear();
    }

    private void CheckSuccess()
    {
        if (correctSwitches == correctSequence.Length)
        {
            Success = true;
            Debug.Log("Successful");
        }
    }

    private void resetSwitches()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].engaged = false;
        }
    }

    IEnumerator CheckcoolDown()
    {
        yield return new WaitForSeconds(0.75f);
        resetSwitches();
    }
   


}
