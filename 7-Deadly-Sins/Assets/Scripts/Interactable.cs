using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    Transform player;
    protected bool hasInteracted = false;
    public Transform interactionTransform;
    

    public virtual void Interact()
    {
        //Debug.Log("Interact with " + transform.name);
    }
    public void OnFocus(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void UnFocus()
    {
        hasInteracted = false;
        isFocus = false;
        player = null;
    }

    protected virtual void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Debug.Log("Interacting with " + name);
                Interact();
                hasInteracted = true;
               
            }
        }
    }

    // will draw the interactable zone as a sphere.
    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    

    
}
