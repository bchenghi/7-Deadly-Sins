using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    Transform CompanionTransform;
    public bool Agressive;
    CompanionController controller;

    private void Start()
    {
        controller = GetComponent<CompanionController>();
        CompanionTransform = this.transform;
        
    }

    private void OnTriggerStay(Collider other)
    {
      
        //if not Aggressive, only attack if the enemy is targeting the player
        if (!Agressive)
        {
            if (other.GetComponent<EnemyController>())
            {
                if (other.GetComponent<EnemyController>().PlayerTargeted)
                {
                    //Make enemy target companion
                    other.GetComponent<EnemyController>().CompanionTargeted = true;
                    other.GetComponent<EnemyController>().PlayerCompanionTarget = CompanionTransform;

                    if (other.GetComponent<EnemyStats>().currentHealth <= 0)
                    {
                        
                        controller.target = null;
                        other.GetComponent<EnemyController>().CompanionTargeted = false;


                        //SwitchTarget
                    }
                    else
                    {

                        controller.target = other.transform;

                    }
                }
            }
        } else
        {
            if (other.GetComponent<EnemyController>())
            {
                //make enemy target companion
                other.GetComponent<EnemyController>().CompanionTargeted = true;
                other.GetComponent<EnemyController>().PlayerCompanionTarget = CompanionTransform;
                //Agrressive means it will attack as long as within distance
                if (other.GetComponent<EnemyStats>().currentHealth <= 0)
                {

                    controller.target = null;
                    other.GetComponent<EnemyController>().CompanionTargeted = false;
                    
                    //SwitchTarget
                }
                else
                {

                    controller.target = other.transform;

                }
            }


        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyController>())
        {
            

                controller.target = null;
            
        }
    }

    




}
