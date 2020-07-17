using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Chooses a dialogue from an array of dialogues based on what previous scene, and displays it.
public class WinSceneManager : Monobehaviour
{
    public GameObject dialogueBox;
    [SerializeField]
    float typeSpeed;
    [SerializeField]
    string nextSceneName;
    TextMeshProUGUI nameText;
    TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    Coroutine currentCoroutine = null;
    public DialogueAfterWin[] dialogues;
    DialogueManager dialogueManager;

    Dialogue dialogue;
    bool doneTyping = true;
    string currentSentence;

    // Start dialogue based on what the previous scene was
    void Start() {
        sentences = new Queue<string>();
        nameText = dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = dialogueBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        dialogue = GetDialogue();

        if (dialogue != null)
        {
            StartExplanation(dialogue);
        }
        else
        {
            Debug.LogWarning("Could not find dialogue to show");
        }

    }


    // Returns a dialogue to show depending on previous scene.
    Dialogue GetDialogue() {
        Dialogue dialogue = null;
        foreach(DialogueAfterWin dialogueAfterWin in dialogues) {
            Debug.Log("dialogueAfterWin.previousSceneName" + dialogueAfterWin.previousSceneName);
            Debug.Log("previous scene name"+PreviousScene.instance.previousSceneName);
            if (dialogueAfterWin.previousSceneName == PreviousScene.instance.previousSceneName) {
                dialogue = dialogueAfterWin.dialogue;
                break;
            }
        }
        return dialogue;
    }

    void StartExplanation(Dialogue dialogue) {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in  dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1/typeSpeed);
        }
        doneTyping = true;
    }
    public void DisplayNextSentence() {
         if (doneTyping && sentences.Count == 0) {
            EndExplanation();
            return;
        }
        else
        {
            if (doneTyping) {
                doneTyping = false;
                string sentence = sentences.Dequeue();
                currentSentence = sentence;
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine);

                currentCoroutine = StartCoroutine(TypeSentence(sentence));
            } else {
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine);
                dialogueText.text = currentSentence;
                doneTyping = true;
            }

        }
    }

    void EndExplanation() 
    {
        // change button to invoke change scene
        SceneLoader.instance.LoadWithoutStats(nextSceneName);
    }

    
}
