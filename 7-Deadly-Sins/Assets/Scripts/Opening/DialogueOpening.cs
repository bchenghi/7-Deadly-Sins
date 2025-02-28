﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using UnityEngine.UI;

public class DialogueOpening : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public Animator animationOpening;


    private void Start()
    {
        StartCoroutine(Type());
        AudioManager.instance.Play("Background Piano");
        AudioManager.instance.Play("Rain");
        AudioManager.instance.Play("Thunder");
        
    }


    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        
        animationOpening.SetTrigger("Change");
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else
        {
            textDisplay.text = "";
        }
    }
}
