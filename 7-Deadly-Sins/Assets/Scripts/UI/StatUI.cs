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
    protected virtual void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
        EquipmentManager.instance.onEquipmentChanged += UpdateStatUI;
        displayText = GetComponentInChildren<Text>();
    }

    // When equipment is updated, text in display will be updated
    public virtual void UpdateStatUI(Equipment newEquipment, Equipment oldEquipment)
    {
    }

    // Returns edited string
    public virtual string textEditor(string text)
    {
        return null;
    }
}
