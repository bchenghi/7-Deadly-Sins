using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Updates stats ui
public class StatUI : MonoBehaviour
{
    protected CharacterStats playerStats;
    protected Text displayText;

    // Start is called before the first frame update
    void Awake() {
        displayText = GetComponentInChildren<Text>();
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }
    protected virtual void Start()
    {
    }

    // When equipment is updated, text in display will be updated
    public virtual void UpdateStatUI()
    {
    }

    // Returns edited string
    public virtual string textEditor(string text)
    {
        return null;
    }
}
