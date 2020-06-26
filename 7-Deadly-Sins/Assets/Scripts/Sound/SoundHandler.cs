using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.Audio;

public class SoundHandler : MonoBehaviour
{

    AudioManager audioManager;


    private void Start()
    {
        audioManager = AudioManager.instance;
    }



    public void PlaySlashSound()
    {
        int index = Random.Range(1, 3);
        if (index == 1)
        {
            audioManager.Play("Slash1");
        } else
        {
            audioManager.Play("Slash2");
        }
    }

    public void PlayPunchSound()
    {
        int index = Random.Range(1, 3);
        if (index == 1)
        {
            
            audioManager.Play("Punch");
        } else
        {
            
            audioManager.Play("Kick");
        }
    }

    public void PlaySoundByName(string name)
    {
        audioManager.Play(name);
    }

    public void Play2SoundRandomly(string name1, string name2)
    {
        int index = Random.Range(1, 3);
        if (index == 1)
        {
       
            audioManager.Play(name1);
        }
        else
        {
           
            audioManager.Play(name2);
        }
    }
    
}
