using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Each slot has a tooltip window, displays text when mouse is over the slot
public class ToolTipWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ToolTip toolTip;
    UISlot UISlot;
    Skill skill;
    HackAndSlashUiSlot hackSlashSlot;
    string stringToShow = null;
    bool mouseCurrentlyHere = false;


    // Start is called before the first frame update
    void Start()
    {
        toolTip = ToolTip.instance;
        UISlot = GetComponent<UISlot>();
        skill = GetComponent<Skill>();
        hackSlashSlot = GetComponent<HackAndSlashUiSlot>();
    }

    void Update()
    {
        if (mouseCurrentlyHere)
        {
            if (stringToShow != "")
            {
                
                toolTip.ShowToolTip(stringToShow);
                CheckSlot();
            }
            else
            {
                toolTip.HideToolTip();
            }
        }
    }

    //  When mouse is over the slot, runs CheckSlot
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mouseCurrentlyHere = true;
        CheckSlot();
    }

    // Hides tooltip when mouse no longer over object
    public void OnPointerExit(PointerEventData pointerEventData){
        mouseCurrentlyHere = false;
        toolTip.HideToolTip();
    }

    //Hides tooltip when the slot is diabled
    public void OnDisable()
    {
        mouseCurrentlyHere = false;
        toolTip.HideToolTip();
    }

    // Sets the string to be shown on tooltip, sets null if object in slot is null
    public void CheckSlot()
    {
        Skill skills = null;
        Item item = null;
        HackAndSlashUiSlot hackSlashSlots = null;
        if (UISlot is EquipmentUISlot)
        {
            item = ((EquipmentUISlot)UISlot).equipment;
        }
        else if (UISlot is InventorySlot)
        {
            item = ((InventorySlot)UISlot).item;
        } else if (UISlot is ShopSlot) 
        {
            item = ((ShopSlot) UISlot).item;
        } else
        {
            if (skill != null)
            {
                skills = skill;
            } else if (hackSlashSlot != null)
            {
                hackSlashSlots = hackSlashSlot;
            }
        }
        if (item != null)
        {
            stringToShow = StatsString(item);
        }else if (skill != null)
        {
            stringToShow = StatsString(skill);
        }else if (hackSlashSlots != null)
        {
            stringToShow = StatsString(hackSlashSlots);
        }else
        {
            stringToShow = "";
        }
    }


    // Returns a string to be displayed on tooltip, empty string if argument is null,
    // or argument is neither equipment, consumable nor item
    string StatsString(Item item)
    {
        StringBuilder result;
        if (item is Equipment)
        {
            result = new StringBuilder(EquipmentStatsString((Equipment)item));
        }
        else if (item is Consumables)
        {
            result = new StringBuilder(ConsumableStatsString((Consumables)item));
        }
        else if (item is Item)
        {
            result = new StringBuilder(ItemStatsString(item));
        }
        else
        {
            result = new StringBuilder();
        }

        if (UISlot is ShopSlot && item != null)
            result.AppendLine().Append(PriceString(item));

        if (SceneManager.GetActiveScene().name == "Shop-CH" && (UISlot is InventorySlot)) {
            InventorySlot currentSlot = (InventorySlot) UISlot;
            result.AppendLine().Append("<color=yellow>Sell for: ").Append(SellManager.instance.SellPrice(item, 1)).Append("</color>");
        }
        return result.ToString();
    }

    string StatsString(HackAndSlashUiSlot slot)
    {
        StringBuilder result;
        if (slot != null)
        {
            result = new StringBuilder(HackAndSlashSlotDescription(slot));
        } else
        {
            result = new StringBuilder();
        }

        return result.ToString();
    }

    string StatsString(Skill skill)
    {
        StringBuilder result;
        if (skill != null)
        {
            result = new StringBuilder(SkillDescription(skill));
        } else
        {
            result = new StringBuilder();
        }

        return result.ToString();
    }

    // Returns string of equipment stats, return null if equipment is null
    string EquipmentStatsString(Equipment equipment)
    {
        StringBuilder str = new StringBuilder();
        if (equipment != null)
        {
            string name = equipment.name;
            int currentLevel = equipment.level;
            int armorModifier = equipment.ArmorModifier();
            int damageModifier = equipment.DamageModifier();

            str.Append(name).AppendLine();
            str.Append("Current Level: ").Append(currentLevel + 1).AppendLine();
            str.Append("<color=green>Armor Modifier: ").Append(armorModifier).Append("</color>").AppendLine();
            str.Append("<color=red>Damage Modifier: ").Append(damageModifier).Append("</color>");
            
            return str.ToString();
        }
        else
        {
            return null;
        }
    }

    // Returns name of item, null if item is null
    string ItemStatsString(Item item)
    {
        StringBuilder str = new StringBuilder();
        if (item != null)
            return item.name;

        return null;
    }

    // Returns stats increase of consumable, null if consumable is null
    string ConsumableStatsString(Consumables consumable)
    {

        if (consumable != null && consumable is Potions)
        {
            Potions potion = (Potions) consumable;
            StringBuilder str = new StringBuilder();
            string name = potion.name;
            string statsBoost = potion.increaseStats.ToString();
            
            str.Append(name).AppendLine();

            if (potion.Health) {
                str.Append("<color=green>Health Boost: +").Append(statsBoost).Append("</color>");
            }
            else {
                str.Append("<color=blue>Mana Boost: +").Append(statsBoost).Append("</color>");
            }
            
            return str.ToString();
        } 
        else if (consumable != null) {
            StringBuilder str = new StringBuilder();
            string name = consumable.name;
            str.Append(name);
            return str.ToString();
        }
        else
        {
            return null;
        }

    }

    string PriceString(Item item) {
        StringBuilder str = new StringBuilder();
        if (item != null) 
        {
            str.Append("<color=yellow>Price: ").Append(item.GetPrice()).Append("</color>");
        } 
        else 
        {
            return null;
        }

        return str.ToString();
    }

    string SkillDescription(Skill skill)
    {
        StringBuilder str = new StringBuilder();
        if (skill != null)
        {
            str.Append("<color=#FB9298>Description: ").Append(skill.Description).Append("</color>");
        } else
        {
            return null;
        }

        return str.ToString();
    }

    string HackAndSlashSlotDescription(HackAndSlashUiSlot slot)
    {
        StringBuilder str = new StringBuilder();
        if (slot != null)
        {
            str.Append("<color=white>Description: ").Append(slot.description).Append("</color>").AppendLine();
            str.Append("<color=white>Price: ").Append(slot.price).Append("</color>");
        } else
        {
            return null;
        }

        return str.ToString();

    }
}
