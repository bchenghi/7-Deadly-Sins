using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    TextMeshProUGUI nameText;
    TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        nameText = dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = dialogueBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueBox.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in  dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    } 

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
        }
    }

    public void EndDialogue() {
        dialogueBox.SetActive(false);
        Debug.Log("End of Conversation");
    }
}
