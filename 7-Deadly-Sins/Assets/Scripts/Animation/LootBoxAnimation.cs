using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxAnimation : MonoBehaviour
{
    CharacterCombat combat;
    Animator animator;

    private void Start()
    {
        combat = PlayerManager.instance.GetComponent<CharacterCombat>();
        animator = GetComponent<Animator>();
        combat.OnAttack += OnAttackLootBox;

    }

    protected virtual void OnAttackLootBox()
    {
        animator.SetTrigger("OnHit");
    }
}
