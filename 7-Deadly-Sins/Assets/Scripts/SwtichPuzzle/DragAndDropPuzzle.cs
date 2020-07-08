using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropPuzzle : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;
    List<RaycastResult> raycastResults;
    private Vector3 RightPosition;
    public bool inRightPosition;
   

    private void Start()
    {
        RightPosition = transform.position;
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(1662, 1880), Random.Range(-600, 240), transform.position.z );
        graphicRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!inRightPosition)
        {
            Debug.Log("OnBeginDrag");
            itemBeingDragged = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(transform.root);
        }

}

    public void OnDrag(PointerEventData eventData)
    {
        if (!inRightPosition)
        {
            Debug.Log("OnDrag");
            transform.position = Input.mousePosition;
            transform.SetParent(startParent);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      
        
        if (Vector3.Distance(transform.position, RightPosition) < 20f)
        {
            transform.position = RightPosition;
            inRightPosition = true;
            transform.SetParent(startParent);
        } else
        {
            transform.position = startPosition;
        }

        /*if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        */
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

       
    }

   

    
}
