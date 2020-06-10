using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (playerCombat != null && myStats.currentHealth > 0)
        {
            //if there is a special being used
            if (playerCombat.SpecialActivated == true)
            {
                //only nextattack is recorded
                if (playerCombat.count == 0)
                {
                    playerCombat.count++;
                    playerCombat.Attack(myStats);
                    StartCoroutine(AnimationBufferForSmashDown());
                }
            } else
            {
                playerCombat.Attack(myStats);
            }
            
           



        }
    }

    //buffer for special to be used.
    IEnumerator AnimationBufferForSmashDown()
    {
        yield return new WaitForSeconds(4);
        playerManager.player.GetComponent<CharacterCombat>().SpecialActivated = false;
        playerManager.player.GetComponent<CharacterCombat>().count = 0;

    }
}
