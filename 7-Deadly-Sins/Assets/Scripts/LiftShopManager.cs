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
    public Timer time;
    public Image[] crossesForFlameThrower;
    private bool boughtFlameThrower;
   

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
    }


    public void ButtonForNonRegenAndPassive(int cost)
    {
        if (!companionManager.maxCompanionReached)
        {
            if (GoldCounter.instance.gold >= cost)
            {
                companion.GetComponent<CompanionStats>().RegenerativeCompanion = false;
                companion.GetComponent<TargetDetector>().Agressive = false;
                GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
                Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
                go.transform.position = newPos;
                GoldCounter.instance.Spend(cost);
                companionManager.AddCompanionToList(go);
            }
        }

    }

    public void ButtonForNonRegenAndAggressive(int cost)
    {
        if (!companionManager.maxCompanionReached)
        {
            if (GoldCounter.instance.gold >= cost)
            {
                companion.GetComponent<CompanionStats>().RegenerativeCompanion = false;
                companion.GetComponent<TargetDetector>().Agressive = true;
                GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
                Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
                go.transform.position = newPos;
                GoldCounter.instance.Spend(cost);
                companionManager.AddCompanionToList(go);
            }
        }
    }

    public void ButtonForRegenAndPassive(int cost)
    {
        if (!companionManager.maxCompanionReached)
        {
            if (GoldCounter.instance.gold >= cost)
            {
                companion.GetComponent<CompanionStats>().RegenerativeCompanion = true;
                companion.GetComponent<TargetDetector>().Agressive = false;
                GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
                Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
                go.transform.position = newPos;
                GoldCounter.instance.Spend(cost);
                companionManager.AddCompanionToList(go);
            }
        }
    }

    public void ButtonForRegenAndAggressive(int cost)
    {
        if (!companionManager.maxCompanionReached)
        {
            if (GoldCounter.instance.gold >= cost)
            {
                companion.GetComponent<CompanionStats>().RegenerativeCompanion = true;
                companion.GetComponent<TargetDetector>().Agressive = true;
                GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
                Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
                go.transform.position = newPos;
                GoldCounter.instance.Spend(cost);
                companionManager.AddCompanionToList(go);
            }
        }
    }

    public void ButtonForFlameThrower(string timePeruseAndCost)
    {

        string[] arr = timePeruseAndCost.Split(","[0]);
        float timePerUse = float.Parse(arr[0]);
        int cost = int.Parse(arr[1]);
        if (GoldCounter.instance.gold >= cost) {
            if (!boughtFlameThrower)
            {
                foreach (Image im in crossesForFlameThrower)
                {
                    im.enabled = true;
                }
                player.GetComponent<PlayerFlameThrower>().UseTimePerAmmo = timePerUse;
                Inventory.instance.Add(flameThrower);
                boughtFlameThrower = true;
                GoldCounter.instance.Spend(cost);
            }
            
        }
    }

    public void ButtonForFlameThrowerAmmo(string quantityAndCost)
    {
        string[] arr = quantityAndCost.Split(","[0]);
        int quantity = int.Parse(arr[0]);
        int cost = int.Parse(arr[1]);
        if (GoldCounter.instance.gold >= cost)
        {
            var ammo = flameThrowerAmmo as Others;
            ammo.quantity = quantity;
            Inventory.instance.Add(ammo);
            GoldCounter.instance.Spend(cost);
        }
    }


    public void ButtonToCloseShopUI()
    {
        shopUI.SetActive(false);
        liftDoor.GetComponent<Animator>().SetBool("DoorOpen", true);
        time.StartTime();

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
