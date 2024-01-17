using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIController : MonoBehaviour
{
    [SerializeField] TMP_Text messageText;
    [SerializeField] string message;

    [SerializeField] int wordIndex;

    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    private void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("words", wordIndex);
            Interact(true);
            GameManager.Instance.Instance_TTS.Speak(dialogue.sentences[0]);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact(false);
        }
    }

    private void Interact(bool show)
    {
        // Start Dialgoue
        if(show)
        {
            dialogueManager.StartDialogue(dialogue);
        }
        else
        {
            dialogueManager.EndDialogue(show);
           
        }
       
    }
}
