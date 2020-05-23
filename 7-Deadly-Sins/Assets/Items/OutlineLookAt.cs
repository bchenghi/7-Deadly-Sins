using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineLookAt : MonoBehaviour
{
    
   

    private OutlineController prevController;
    private OutlineController currentController;

    private void Update()
    {
        HandleLookAtRay();
    }

    private void HandleLookAtRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        int playerLayerMask = LayerMask.GetMask("Player");
        if (Physics.Raycast(ray, out hit, 100, ~playerLayerMask))
        {
            if (hit.collider.CompareTag("Interact"))
            {
                currentController = hit.collider.GetComponent<OutlineController>();

                if (prevController != currentController)
                {
                    HideOutline();
                    ShowOutline();
                }

                prevController = currentController;
            }
            else
            {
                HideOutline();
            }
        }
        else
        {
            HideOutline();
        }
    }

    private void ShowOutline()
    {
        if (currentController != null)
        {
            currentController.ShowOutline();
            Debug.Log("outline shown");
        }
    }

    private void HideOutline()
    {
        if (prevController != null)
        {
            prevController.HideOutline();
            prevController = null;
            Debug.Log("outline hidden");
        }
    }
}
