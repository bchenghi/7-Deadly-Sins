using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxAnimation : MonoBehaviour
{
    CharacterCombat combat;
    Animator animator;
    Chest chest;

    private void Start()
    {
        combat = PlayerManager.instance.GetComponent<CharacterCombat>();
        animator = GetComponent<Animator>();
        chest = GetComponent<Chest>();


        chest.CanOpen += hasKeyAnimate;
        chest.CanOpen += hasPermissionAnimate;
    


    }

    
    public void hasKeyAnimate() {

        
            animator.SetBool("HasKey", true);
            Debug.Log("has key is true");
        
            
        }

    public void hasPermissionAnimate()
    {

        
            animator.SetTrigger("OnHit");
            Debug.Log("On hit triggered");
        
    }
    
    


       



    
}
