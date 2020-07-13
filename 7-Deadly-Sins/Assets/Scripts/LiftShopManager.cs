using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftShopManager : MonoBehaviour
{
    public GameObject companion;
    public Item flameThrower;
    public Item flameThrowerAmmo;
    Transform player;
    public GameObject shopUI;
    public Transform liftDoor;
    public LiftMovement movement;
    private bool UIShownLevel2;
    private bool UIShownLevel3;
    public CompanionsManager companionManager;
    public Image[] crossesForFlameThrower;
    private bool boughtFlameThrower;

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
    }


    public void ButtonForNonRegenAndPassive()
    {
        if (!companionManager.maxCompanionReached)
        {
            companion.GetComponent<CompanionStats>().RegenerativeCompanion = false;
            companion.GetComponent<TargetDetector>().Agressive = false;
            GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
            Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
            go.transform.position = newPos;
            companionManager.AddCompanionToList(go);
        }

    }

    public void ButtonForNonRegenAndAggressive()
    {
        if (!companionManager.maxCompanionReached)
        {
            companion.GetComponent<CompanionStats>().RegenerativeCompanion = false;
            companion.GetComponent<TargetDetector>().Agressive = true;
            GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
            Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
            go.transform.position = newPos;
            companionManager.AddCompanionToList(go);
        }
    }

    public void ButtonForRegenAndPassive()
    {
        if (!companionManager.maxCompanionReached)
        {
            companion.GetComponent<CompanionStats>().RegenerativeCompanion = true;
            companion.GetComponent<TargetDetector>().Agressive = false;
            GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
            Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
            go.transform.position = newPos;
            companionManager.AddCompanionToList(go);
        }
    }

    public void ButtonForRegenAndAggressive()
    {
        if (!companionManager.maxCompanionReached)
        {
            companion.GetComponent<CompanionStats>().RegenerativeCompanion = true;
            companion.GetComponent<TargetDetector>().Agressive = true;
            GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
            Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
            go.transform.position = newPos;
            companionManager.AddCompanionToList(go);
        }
    }

    public void ButtonForFlameThrower(float timePerUse)
    {
        if (!boughtFlameThrower)
        {
            foreach (Image im in crossesForFlameThrower)
            {
                im.enabled = true;
            }
            player.GetComponent<PlayerFlameThrower>().UseTimePerAmmo = timePerUse;
            Inventory.instance.Add(flameThrower);
            boughtFlameThrower = true;
        }
    }

    public void ButtonForFlameThrowerAmmo(int quantity)
    {
        var ammo = flameThrowerAmmo as Others;
        ammo.quantity = quantity;
        Inventory.instance.Add(ammo);
    }


    public void ButtonToCloseShopUI()
    {
        shopUI.SetActive(false);
        liftDoor.GetComponent<Animator>().SetBool("DoorOpen", true);

    }

    private void Update()
    {
        if (movement.reachedLevel2 && !UIShownLevel2)
        {
            shopUI.SetActive(true);
            UIShownLevel2 = true;
        }

        if (movement.reachedLevel3 && !UIShownLevel3)
        {
            UIShownLevel3 = true;
            shopUI.SetActive(true);
        }
    }
}
