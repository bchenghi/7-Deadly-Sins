using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SlotDrop : MonoBehaviour, IDropHandler
{
 

    public void OnDrop(PointerEventData eventData)
    {
        
            DragDrop.itemBeingDragged.transform.SetParent(transform);
            
        
    }
}
