using System.Collections;
using System.Collections.Generic;
using System.Text;

// Gets damage stat from player, then displays in stat area in UI
public class DamageStatUI : StatUI
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        displayText.text = textEditor(playerStats.damage.GetValue().ToString());
    }

    // When equipment is updated, text in display will be updated
    public override void UpdateStatUI(Equipment newEquipment, Equipment oldEquipment)
    {
        displayText.text = textEditor(playerStats.damage.GetValue().ToString());
    }

    // Returns edited string
    public override string textEditor(string text)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<color=red>").Append(text).Append("</color>");
        return str.ToString();
    }
}
