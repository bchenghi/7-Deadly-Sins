using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;
    List<RaycastResult> raycastResults;

    bool clicked = false;
    
    

    void Start()
    {
        graphicRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
    }

  

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        clicked = false;
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
                //Debug.Log(result);
                if (result.gameObject.CompareTag("Hotkey"))
                {




                    if (itemBeingDragged.GetComponentInParent<InventorySlot>().item is IUsable)
                    {


                        var obj = itemBeingDragged.GetComponentInParent<InventorySlot>().item;
                        var castedObj = obj as IUsable;

                        result.gameObject.GetComponentInParent<HotKey>().RemoveFromHotKey();
                        result.gameObject.GetComponentInParent<HotKey>().SetUsable(castedObj);
                        HotKeyBar.instance.HotKeyBarRearranged();
                        break;
                    }





                }
                else if (result.gameObject.CompareTag("SellButton"))
                {
                    Vector3 scale = transform.localScale;
                    SellManager.instance.itemTobeSold = itemBeingDragged.GetComponentInParent<InventorySlot>().item;
                    Item item = itemBeingDragged.GetComponentInParent<InventorySlot>().item;
                    if (itemBeingDragged.GetComponentInParent<InventorySlot>().count == 1 || itemBeingDragged.GetComponentInParent<InventorySlot>().consumableCounter.text == "" )
                    {
                        StartCoroutine(hideImage(transform, scale));
                        SellManager.instance.SellSingleItem(item);
                        
                        
                    }
                    else
                    {

                        result.gameObject.transform.GetComponent<SellButton>().OpenQuanityText();
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
        clicked = true;
        
        
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (clicked) {
            GetComponentInParent<Button>().onClick.Invoke();
        }
    }

    IEnumerator hideImage(Transform transform, Vector3 scale)
    {
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = scale;
    }



    
}
