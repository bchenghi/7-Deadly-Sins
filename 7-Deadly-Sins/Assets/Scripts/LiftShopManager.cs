using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftShopManager : MonoBehaviour
{
    public GameObject companion;
    public Item flameThrower;
    public Item flameThrowerAmmo;
    Transform player;
    public GameObject shopUI;
    public Transform liftDoor;

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
    }


    public void ButtonForNonRegenAndPassive()
    {
        companion.GetComponent<CompanionStats>().RegenerativeCompanion = false;
        companion.GetComponent<TargetDetector>().Agressive = false;
        GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
        Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
        go.transform.position = newPos;

    }

    public void ButtonForNonRegenAndAggressive()
    {
        companion.GetComponent<CompanionStats>().RegenerativeCompanion = false;
        companion.GetComponent<TargetDetector>().Agressive = true;
        GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
        Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
        go.transform.position = newPos;
    }

    public void ButtonForRegenAndPassive()
    {
        companion.GetComponent<CompanionStats>().RegenerativeCompanion = true;
        companion.GetComponent<TargetDetector>().Agressive = false;
        GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
        Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
        go.transform.position = newPos;
    }

    public void ButtonForRegenAndAggressive()
    {
        companion.GetComponent<CompanionStats>().RegenerativeCompanion = true;
        companion.GetComponent<TargetDetector>().Agressive = true;
        GameObject go = GameObject.Instantiate(companion, transform.position, Quaternion.identity);
        Vector3 newPos = new Vector3(player.position.x + Random.Range(-4f, 4f), player.position.y, player.position.z + 2f);
        go.transform.position = newPos;
    }

    public void ButtonForFlameThrower(float timePerUse)
    {
        player.GetComponent<PlayerFlameThrower>().UseTimePerAmmo = timePerUse;
        Inventory.instance.Add(flameThrower);
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
}
