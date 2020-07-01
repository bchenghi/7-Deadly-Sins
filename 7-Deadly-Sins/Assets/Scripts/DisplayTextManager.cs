using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayTextManager : MonoBehaviour
{
    public static DisplayTextManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("More than one instance of display text found");
            return;
        }
    }

    [SerializeField]
    GameObject displayTextObject;
    TextMeshProUGUI displayText;

    [SerializeField]
    float fadeInTime = 0.4f;
    [SerializeField]
    float fadeOutTime = 0.4f;

    Coroutine currentCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        displayText = displayTextObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display(string text, float durationTextDisplayed) {
        displayText.text = text;
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(DisplayText(text, durationTextDisplayed));
    }

    IEnumerator FadeIn(float timeTakenToFadeIn) {
        displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, 0);
        while(displayText.color.a < 1.0f) {
            displayText.color = new Color(displayText.color.r, displayText.color.g, 
            displayText.color.b, displayText.color.a + (Time.deltaTime / timeTakenToFadeIn));
            yield return null;
        }
    }

    IEnumerator FadeOut(float timeTakenToFadeOut) {
        displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, 1f);
        while(displayText.color.a > 0) {
            displayText.color = new Color(displayText.color.r, displayText.color.g, 
            displayText.color.b, displayText.color.a - (Time.deltaTime / timeTakenToFadeOut));
            yield return null;
        }
    }

    IEnumerator DisplayText(string text, float durationTextDisplayed) {
        displayTextObject.SetActive(true);
        yield return FadeIn(fadeInTime);
        yield return new WaitForSeconds(durationTextDisplayed);
        yield return FadeOut(fadeOutTime);
        displayTextObject.SetActive(false);
    }

}
