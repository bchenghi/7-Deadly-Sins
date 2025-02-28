﻿
using UnityEngine;

public class SoundHandler : MonoBehaviour
{

    AudioManager audioManager;


    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    // Checks what the argument was, if it is player will play attack sound by player, 
    // else, plays default atack sound
    public void PlayAttackSoundBy(Transform transformOfObject) {
        if (transformOfObject.name == "Player") {
            PlayerAttackSound();
        } else if (transformOfObject.GetComponent<Enemy>()){
            EnemyAttackSound(transformOfObject);
        } else {
            DefaultAttackSound(transform);
        }
    }


    void PlayerAttackSound() {
        if (EquipmentManager.instance.currentEquipment[EquipmentManager.instance.GetWeaponIndex()] != null){
            PlaySlashSound(PlayerManager.instance.player.transform);
        } else {
            PlayPunchSound(PlayerManager.instance.player.transform);
        }
    }

    void EnemyAttackSound(Transform transformOfEnemy) {
        if (transformOfEnemy.GetComponent<Enemy>().name == "Palladin") {
            PlaySlashSound(transformOfEnemy);
        } else {
            PlayPunchSound(transformOfEnemy);
        }
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
    
    // Takes a transform as argument to attach an audio source to play sound
    public void PlaySlashSound(Transform transformOfObject)
    {
        int index = Random.Range(1, 3);
        if (index == 1)
        {
            audioManager.Play(transformOfObject, "Slash1");
        } else
        {
            audioManager.Play(transformOfObject, "Slash2");
        }
    }

    // Takes a transform as argument to attach an audio source to play sound
    public void PlayPunchSound(Transform transformOfObject)
    {
        int index = Random.Range(1, 3);
        if (index == 1)
        {
            
            audioManager.Play(transformOfObject, "Punch");
        } else
        {
            
            audioManager.Play(transformOfObject, "Kick");
        }
    }

    // Takes a transform as argument to attach an audio source to play sound
    void DefaultAttackSound(Transform transform) {
        PlayPunchSound(transform);
    }
    

    public void PlaySoundByName(string name)
    {
        audioManager.Play(name);
    }

    // Same as playSoundByName, but creates audio source in transform of object instead of gameManager
    public void PlaySoundByName(Transform transformOfObject, string name) {
        audioManager.Play(transformOfObject, name);
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

    // Input arbitrary number of arguments, plays one of them randomly
    public void PlaySoundRandomly(params string[] names) {
        int lengthOfArray = names.Length;
        int index = Random.Range(0, lengthOfArray);
        audioManager.Play(names[index]);
    }

    // Random sound played from array of sounds, audio source placed on given transform
    public void PlaySoundRandomly(string[] names, Transform transformOfObj) {
        int lengthOfArray = names.Length;
        int index = Random.Range(0, lengthOfArray);
        if (audioManager == null) {  
            AudioManager.instance.Play(transformOfObj, names[index]);
        } else {
            audioManager.Play(transformOfObj, names[index]);
        }
    }
   
}
