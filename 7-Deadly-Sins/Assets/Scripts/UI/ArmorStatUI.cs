using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Gets armor stat from player, then displays in stat area in UI
public class ArmorStatUI : StatUI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        displayText.text = textEditor(playerStats.armor.GetValue().ToString());
    }

    // When equipment is updated, text in display will be updated
    public override void UpdateStatUI()
    {
        displayText.text = textEditor(playerStats.armor.GetValue().ToString());
    }

    // Returns edited string
    public override string textEditor(string text)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<color=lime>").Append(text).Append("</color>");
        return str.ToString();
    }

}
