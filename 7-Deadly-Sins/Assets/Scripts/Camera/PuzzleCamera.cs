using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCamera : MonoBehaviour
{
    public GameObject puzzleUI;

    private void LateUpdate()
    {
        if (puzzleUI.activeSelf)
        {
            GetComponent<ParentCameraController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            GetComponent<ParentCameraController>().enabled = true;
            
        }
    }
}
