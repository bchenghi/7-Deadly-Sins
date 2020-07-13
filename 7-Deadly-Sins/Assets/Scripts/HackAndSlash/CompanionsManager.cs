using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CompanionsManager : MonoBehaviour
{
    public static CompanionsManager instance { get; private set; }
    public int maxCompanions;
    private int currentCompanions;
    public List<GameObject> CompanionsInScene;
    public bool maxCompanionReached;
    public TextMeshProUGUI text;
    public LiftMovement liftMovement;
    private bool companionsTeleportedto2;
    private bool companionsTeleportedto3;
    public float teleportUpDistance;
    Color color;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;


        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        CompanionsInScene = new List<GameObject>();
        text.text = "Max: " + maxCompanions;
        color = text.color;
    }

    private void Update()
    {
        if (currentCompanions == maxCompanions)
        {
            maxCompanionReached = true;
            text.color = Color.red;
        } else
        {
            maxCompanionReached = false;
            text.color = color;
        }

        if (liftMovement.reachedLevel2 && !companionsTeleportedto2)
        {
            for (int i = 0; i < CompanionsInScene.Count; i++)
            {
                
                CompanionsInScene[i].transform.GetComponent<NavMeshAgent>().Warp(new Vector3(CompanionsInScene[i].transform.position.x, CompanionsInScene[i].transform.position.y + teleportUpDistance, CompanionsInScene[i].transform.position.z));
                
               

            }
            companionsTeleportedto2 = true;
        }

        if (liftMovement.reachedLevel3 && !companionsTeleportedto3)
        {
            for (int i = 0; i < CompanionsInScene.Count; i++)
            {

                CompanionsInScene[i].transform.GetComponent<NavMeshAgent>().Warp(new Vector3(CompanionsInScene[i].transform.position.x, CompanionsInScene[i].transform.position.y + teleportUpDistance, CompanionsInScene[i].transform.position.z));



            }
            companionsTeleportedto3 = true;
        }
    }


    public void AddCompanionToList(GameObject companion)
    {
        currentCompanions++;
        if (currentCompanions <= maxCompanions)
        {
            CompanionsInScene.Add(companion);
        } else
        {
            currentCompanions = maxCompanions;
            Debug.Log("Companions exceeded");
        }
    }

    public void DecreaseCompanionInList(GameObject companion)
    {
        currentCompanions--;
        CompanionsInScene.Remove(companion);
    }

    
}
