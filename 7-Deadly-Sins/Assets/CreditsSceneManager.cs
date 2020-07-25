using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSceneManager : MonoBehaviour
{
    [SerializeField]
    string nameOfSound;
    [SerializeField]
    GameObject[] arrayOfGameObjectsToFadeIn;
    [SerializeField]
    float delayBetweenEachFade;
    [SerializeField]
    float fadeInDuration;
    [SerializeField]
    float pauseBeforeStart;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play(nameOfSound);
        StartCoroutine(FadeInAll());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn(GameObject gameObject) {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        while(gameObject.GetComponent<CanvasGroup>().alpha < 1.0f) {
            gameObject.GetComponent<CanvasGroup>().alpha = gameObject.GetComponent<CanvasGroup>().alpha + (Time.unscaledDeltaTime / fadeInDuration);
            yield return null;
        }
    }

    IEnumerator FadeInAll() {
        float timePassed = 0;
        while(timePassed < pauseBeforeStart) {
            timePassed += Time.unscaledDeltaTime;
            yield return null;
        }
        int index = 0;
        int numberOfGameObjects = arrayOfGameObjectsToFadeIn.Length;
        bool currentIndexFadeInStarted = false;
        timePassed = 0;
        while(index < numberOfGameObjects) {
            timePassed += Time.unscaledDeltaTime;
            if (!currentIndexFadeInStarted) {
                StartCoroutine(FadeIn(arrayOfGameObjectsToFadeIn[index]));
                currentIndexFadeInStarted = true;
            }
            if (timePassed >= delayBetweenEachFade) {
                timePassed = 0;
                index++;
                currentIndexFadeInStarted = false;
            }
            yield return null;
        }
    }
}
