using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public PlayerController player;
    private const int MAX_STAMINA = 100;
    private float stamina;
    private float staminaRegen;
    public bool CanRegen;
    Coroutine LookAtCoroutine;
    public bool onlyWalk = false;

    private void Start()
    {
        player = PlayerManager.instance.player.GetComponent<PlayerController>();
        stamina = 100;
        staminaRegen = 10f;
        CanRegen = true;
    }

    public Stamina()
    {
        
    }

    public void Update()
    {
        Debug.Log(stamina);
        if (stamina <= 0.1)
        {
            StartCoroutine(WalkBuffer());
            
        }

        if (onlyWalk == false)
        {
            if (player.running)
            {
                UseStamina(5f * Time.deltaTime);
                CanRegen = false;
                StartLookAt();
            }



            if (CanRegen && player.running == false)
            {
                stamina += staminaRegen * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, MAX_STAMINA);
            }
        } 
    }

    public void UseStamina(float amount)
    {
        if (stamina >= amount)
        {
            stamina -= amount;
        }
        stamina = Mathf.Clamp(stamina, 0, MAX_STAMINA);
    }

    public float GetStaminaNormalized()
    {
        return stamina / MAX_STAMINA;
    }

    IEnumerator RegenBuffer()
    {
        yield return new WaitForSeconds(5);
        CanRegen = true;
    }

    IEnumerator WalkBuffer()
    {
        player.isSlowed = true;
        onlyWalk = true;
        yield return new WaitForSeconds(3);
        onlyWalk = false;
        player.isSlowed = false;
        stamina += 0.1f;
        
        
    }
    

    public void StartLookAt()
    {
        if (LookAtCoroutine != null)
        {
            StopCoroutine(LookAtCoroutine);
        }
        LookAtCoroutine = StartCoroutine(RegenBuffer());
    }















    }
