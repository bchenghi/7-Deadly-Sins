using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;
    List<RaycastResult> raycastResults;
    
    

    void Start()
    {
        graphicRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
    }

  

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent(transform.root);

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("OnEndDrag");
        
        
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
       
            eventData.position = Input.mousePosition;
            raycastResults.Clear();
            graphicRaycaster.Raycast(eventData, raycastResults);
            
            if (raycastResults.Count > 0)
            {
                foreach (var result in raycastResults)
                {
                    Debug.Log(result);
                    if (result.gameObject.CompareTag("Hotkey"))
                    {

                        
                        
                    
                        if (itemBeingDragged.GetComponentInParent<InventorySlot>().item is IUsable)
                        {
                        

                            var obj = itemBeingDragged.GetComponentInParent<InventorySlot>().item;
                            var castedObj = obj as IUsable;

                           
                            result.gameObject.GetComponentInParent<HotKey>().SetUsable(castedObj);
                            break;
                        }
                        
                    
                        
                        
                    
                    }
                }
            }
            itemBeingDragged = null;
            raycastResults.Clear();
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        
        GetComponentInParent<Button>().onClick.Invoke();
    }

    
}
