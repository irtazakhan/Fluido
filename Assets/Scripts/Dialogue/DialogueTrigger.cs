using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.StartDialogue(dialogue);
        }
    }

}
