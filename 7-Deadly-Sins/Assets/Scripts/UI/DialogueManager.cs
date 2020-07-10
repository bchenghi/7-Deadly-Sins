using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance;
    void Awake() {
        if (DialogueManager.instance == null) {
            DialogueManager.instance = this;
        } else {
            Destroy(this);
        }
    }
    #endregion

    public GameObject dialogueBox;
    [SerializeField]
    float typeSpeed;
    TextMeshProUGUI nameText;
    TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    Coroutine currentCoroutine = null;
    bool coroutineTypeSentenceRunning = false;
    string currentSentence = null;

    [HideInInspector]
    public bool currentlyInDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        nameText = dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = dialogueBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void StartDialogue(Dialogue dialogue) {
        currentlyInDialogue = true;
        dialogueBox.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in  dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    } 

    IEnumerator TypeSentence(string sentence) {
        coroutineTypeSentenceRunning = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1/typeSpeed);
        }
        coroutineTypeSentenceRunning = false;
    }

    // Will show entire sentence if sentence is still being typed. If not being
    // typed, will move on to next sentence or end dialogue if no more sentences.
    public void DisplayNextSentence() {
        if (coroutineTypeSentenceRunning) 
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            
            dialogueText.text = currentSentence;
            coroutineTypeSentenceRunning = false;
        }
        else 
        {
            if (sentences.Count == 0) {
                EndDialogue();
                return;
            }
            else 
            {   
                string sentence = sentences.Dequeue();
                currentSentence = sentence;
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine);

                currentCoroutine = StartCoroutine(TypeSentence(sentence));
            }
        }
    }

    public void EndDialogue() {
        dialogueBox.SetActive(false);
        Debug.Log("End of Conversation");
        currentlyInDialogue = false;
    }

 }
