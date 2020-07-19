using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public static ToolTip instance;
    GameObject background;
    private Text toolTipText;
    private RectTransform backgroundRectTransform;
    public RectTransform canvasRectTransform;
    public float textPaddingSize = 4f;

    private void Awake()
    {
        background = transform.Find("Background").gameObject;
        backgroundRectTransform = background.GetComponent<RectTransform>();
        toolTipText = backgroundRectTransform.Find("Text").GetComponent<Text>();

        instance = this;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        // Below is to shift the anchor position when tooltip leaves screen so that it is always visible
        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log("anchored position x " + anchoredPosition.x + " background width " + backgroundRectTransform.rect.width + " canvasRectTransform.rect.width " + canvasRectTransform.rect.width);
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    // Sets tooltip active and sets text via SetText method
    public void ShowToolTip(string toolTipString)
    {
        background.SetActive(true);
        SetText(toolTipString);
    }

    public void HideToolTip()
    {
        background.SetActive(false);
    }

    // Sets text in text box and sets the size of background
    public void SetText(string str)
    {
        toolTipText.text = str;
        Vector2 backgroundSize = new Vector2(toolTipText.preferredWidth +
            textPaddingSize * 2f, toolTipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }
}
