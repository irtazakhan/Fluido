using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    public bool startDialogue;
    private void Start()
    {
        if(startDialogue)
        {
            dialogueManager.StartDialogue(dialogue, false);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(dialogue, false);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
