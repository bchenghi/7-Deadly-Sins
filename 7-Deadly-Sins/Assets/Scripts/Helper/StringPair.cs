using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PreviousNextSceneName
{
    public string previous;
    public string next;
    PreviousNextSceneName(string previous, string next) 
    {
        this.previous = previous;
        this.next = next;
    }
    
}
