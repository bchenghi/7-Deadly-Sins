using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionMarkPlayerDetector : MonoBehaviour
{
    public Transform QuestionText;
    QuestionMark questionMark;

    private void Start()
    {
        questionMark = GetComponent<QuestionMark>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (questionMark.completed && questionMark.checkItem())
            {
                SetCompletedTextFoundItem();
            }
            else if (questionMark.completed && !questionMark.checkItem())
            {
                SetCompletedTextNoItem();
            }else
            {

                SetText();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (questionMark.checkItem())
                    {
                        GetComponent<QuestionMark>().isChecked = true;
                        SetTextToNull();
                    }
                    else
                    {
                        GetComponent<QuestionMark>().isChecked = true;
                        SetTextToNoItemsFound();
                    }
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            SetTextToNull();
        }
    }

    private void SetText()
    {
        QuestionText.GetComponent<TextMeshProUGUI>().text = "Press E to search " + questionMark.LocationName;
    }

    private void SetTextToNull()
    {
        QuestionText.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void SetCompletedTextFoundItem()
    {
        QuestionText.GetComponent<TextMeshProUGUI>().text = questionMark.LocationName + " has been searched, Item Found";
    }

    private void SetTextToNoItemsFound()
    {
        QuestionText.GetComponent<TextMeshProUGUI>().text = "No item Found";
    }

    private void SetCompletedTextNoItem()
    {
        QuestionText.GetComponent<TextMeshProUGUI>().text = questionMark.LocationName + " has been searched, No item Found";
    }


}
