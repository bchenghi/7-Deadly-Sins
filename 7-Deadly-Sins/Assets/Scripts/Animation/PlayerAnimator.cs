using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{   
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationDict;
    SoundHandler soundHandler;

    bool potionUseAnim = false;

    protected override void Start()
    {
        base.Start();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationDict = new Dictionary<Equipment, AnimationClip[]>();

        foreach(WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationDict.Add(a.weapon, a.clips);
        }

        combat.OnAttack += UseSpecial;
        soundHandler = GetComponent<SoundHandler>();
        SceneSetUp();
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        Debug.Log("newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon: "+ (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon));
        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (WeaponsDictionaryContains(newItem))
            {
                currentAttackAnimSet = WeaponsDictionaryGet(newItem);
            }
        } else if (newItem == null && oldItem != null && oldItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
        }

        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.equipmentSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 0);
        }


    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }


    public virtual void UseSpecial()
    {

        animator.SetBool("Special", combat.SpecialActivated);

    }

    public virtual void CastRangeSpell()
    {
        animator.SetTrigger("RangeSpell");
    }

    public void UsePotion()
    {
        animator.SetTrigger("PotionUsed");
        potionUseAnim = true;
    }

    public void KneelDown()
    {
        animator.SetTrigger("KneelDown");
    }

    public void Climbing()
    {
        animator.SetBool("Climbing", GetComponent<PlayerController>().Climbing);
    }

    public void Hook()
    {
        animator.SetBool("Hooked", GetComponent<GrapplingHooks>().fired);
    }


    // When scene is setup, prepares attack animation clips based on current equipment
    // e.g sword attack animation if sword is equipped
    void SceneSetUp() {
        foreach(Equipment equipment in EquipmentManager.instance.currentEquipment) {
            if (equipment != null) {
                this.OnEquipmentChanged(equipment, null);
            }
        }
    }

    bool WeaponsDictionaryContains(Equipment item) {
        bool result = false;
        foreach (KeyValuePair<Equipment, AnimationClip[]> pair in weaponAnimationDict) {
            if (pair.Key.name == item.name) {
                result = true;
                break;
            }
        }
        return result;
    }


    AnimationClip[] WeaponsDictionaryGet(Equipment item) {
        AnimationClip[] animations = null;
        foreach (KeyValuePair<Equipment, AnimationClip[]> pair in weaponAnimationDict) {
            if (pair.Key.name == item.name) {
                animations = pair.Value;
                break;
            }
        }
        return animations;        
    }
}
