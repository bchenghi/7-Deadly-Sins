using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossRoomOpener : MonoBehaviour
{
    public GameObject DoorToOpen;
    EnemyStats enemyStats;
    private bool BossRoomOpen;
    private bool hasOpened;
    public Transform BossSpanwedImage;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (!BossRoomOpen)
        {
            checkHealth();
            
        }

        if (BossRoomOpen && hasOpened == false)
        {
            Debug.Log("Door Open");
            RotateDoor();
            StartCoroutine(WordsCooldown());
            hasOpened = true;
        }
    }


    private void checkHealth()
    {
        if (enemyStats.currentHealth <= 0)
        {
            BossRoomOpen = true;
        }
    }

    private void RotateDoor()
    {
        DoorToOpen.transform.Rotate(new Vector3(0, 0, -60));
        
    }

    IEnumerator WordsCooldown()
    {
        BossSpanwedImage.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(3f);
        BossSpanwedImage.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
