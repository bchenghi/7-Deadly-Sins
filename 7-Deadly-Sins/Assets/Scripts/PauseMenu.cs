using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject controlsUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            if (gameIsPaused) 
            {
                Resume();
            } 
            else 
            {
                Pause();
            }
        }
    }


    public void Resume() 
    {
        controlsUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause() 
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame() {
        Debug.Log("quitting game");
        Application.Quit();
    }

    public void SaveGame() {
        Debug.Log("saved game");
        DisplayTextManager.instance.Display("Game saved!", 2f);
        SaveLoad.instance.Save();
    }

    public void ShowControls() {
        Debug.Log("Show controls");
        PauseMenuUI.SetActive(false);
        controlsUI.SetActive(true);
    }

    public void HideControls() {
        Debug.Log("Hide controls");
        controlsUI.SetActive(false);
        PauseMenuUI.SetActive(true);
    }
}
