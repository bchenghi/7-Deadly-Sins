﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropSkill : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject AllDragDrops;
    ToolTipWindow[] tips;
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
        tips = AllDragDrops.GetComponentsInChildren<ToolTipWindow>();
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
        foreach(ToolTipWindow tip in tips)
        {
            tip.enabled = false;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (ToolTipWindow tip in tips)
        {
            tip.enabled = true;
        }
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
                    HotKey currentHotKey = result.gameObject.GetComponent<HotKey>();
                    if (currentHotKey.isFilled == false || (currentHotKey.isFilled && !currentHotKey.CheckWhetherHotKeyisCoolingDown()))
                    {
                        result.gameObject.GetComponentInParent<HotKey>().RemoveFromHotKey();
                        result.gameObject.GetComponentInParent<HotKey>().SetUsable(itemBeingDragged.GetComponent<IUsable>());
                        itemBeingDragged.transform.GetComponent<DragDropSkill>().enabled = false;
                        //HotKeyBar.instance.HotKeyBarRearranged();
                    } else
                    {
                        Debug.Log("HotKey is in use");
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
    }




}

