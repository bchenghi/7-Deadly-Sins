using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftShopCamera : MonoBehaviour
{
    public GameObject liftShopUi;

    private void LateUpdate()
    {
        if (liftShopUi.activeSelf)
        {
            StartCoroutine(CameraWait());
        }
        else
        {
            GetComponent<ParentCameraController>().enabled = true;

        }
    }

    IEnumerator CameraWait()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<ParentCameraController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

