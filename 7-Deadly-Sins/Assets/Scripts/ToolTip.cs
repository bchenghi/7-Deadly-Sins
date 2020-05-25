using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Text toolTipText;
    private RectTransform backgroundRectTransform;
    public float textPaddingSize = 4f;


    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        toolTipText = backgroundRectTransform.Find("Text").GetComponent<Text>();

        ShowToolTip("random tool tip wordsssssluifalifhirsgn \n s");
    }

    private void Update()
    {
        


        //transform.position = Input.mousePosition;
    }

    private void ShowToolTip(string toolTipString)
    {
        gameObject.SetActive(true);

        toolTipText.text = toolTipString;
        Vector2 backgroundSize = new Vector2(toolTipText.preferredWidth + 
            textPaddingSize * 2f, toolTipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    private void FollowCursor()
    {
        /*
        if (!this.gameObject.activeSelf) { return; }

            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;
            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + backgroundRectTransform.rect.width * backgroundRectTransform.scaleFactor / 2) - padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }
            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - backgroundRectTransform.rect.width * backgroundRectTransform.scaleFactor / 2) + padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }
            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + backgroundRectTransform.rect.height * backgroundRectTransform.scaleFactor) - padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }
            transform.position = newPos;
            */
    }
}
