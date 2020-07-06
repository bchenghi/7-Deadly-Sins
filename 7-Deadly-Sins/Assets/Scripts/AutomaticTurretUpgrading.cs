using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticTurretUpgrading : MonoBehaviour
{
    public GameObject UpgradedTurret;
   

    public int ToolsRequired;
    public Item Tools;
    private bool hasSufficientTools;
    public float UpgradingTime;
    public bool isUpgrading;
    public GameObject UpgradingIcon;
  

    private void Start()
    {
        UpgradingIcon = GameObject.Find("Upgrading");
    }

    private void CheckInventory()
    {
        if (Inventory.instance.getValue(Tools) >= ToolsRequired)
        {
            hasSufficientTools = true;
        } else
        {
            hasSufficientTools = false;
        }
    }

    private void Upgrading()
    {
        RemoveFromInventory();
        StartCoroutine(UpgradingInProgress(UpgradingTime));
        
    }

    IEnumerator UpgradingInProgress(float time)
    {
        UpgradingIcon.transform.GetComponent<Image>().enabled = true;
        isUpgrading = true;
        yield return new WaitForSeconds(time);
        isUpgrading = false;
        UpgradingIcon.transform.GetComponent<Image>().enabled = false;
        GameObject Go = GameObject.Instantiate(UpgradedTurret, transform.position, Quaternion.identity);
        Go.name = UpgradedTurret.name;
        Destroy(this.gameObject);
        
    }

    private void Update()
    {
        CheckInventory();
        if (hasSufficientTools && !isUpgrading)
        {
            float distance = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
            if (Input.GetKeyDown(KeyCode.E) && distance <= 1f)
            {
                Upgrading();
            }
        }
    }

    private void RemoveFromInventory()
    {
        Inventory.instance.Remove(Tools, ToolsRequired);

    }


}
