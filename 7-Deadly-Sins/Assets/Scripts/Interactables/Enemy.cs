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

            if (playerCombat.SpecialActivated == true)
            {
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

    IEnumerator AnimationBufferForSmashDown()
    {
        yield return new WaitForSeconds(4);
        playerManager.player.GetComponent<CharacterCombat>().SpecialActivated = false;
        playerManager.player.GetComponent<CharacterCombat>().count = 0;

    }
}
