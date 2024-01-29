using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;


public class AIController : MonoBehaviour
{
    [SerializeField] TMP_Text messageText;
    [SerializeField] string message;

    [SerializeField] int wordIndex;

    [SerializeField] GameObject interactButton;
    [SerializeField] GameObject player;

    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    private bool isInteractable=true;

    private void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(isInteractable)
            {
                PlayerPrefs.SetInt("words", wordIndex);
                interactButton.SetActive(true);
                interactButton.GetComponent<Button>().onClick.RemoveAllListeners();
                interactButton.GetComponent<Button>().onClick.AddListener(Interact);
                player = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
    }


    private void Interact()
    {
        GameManager.Instance.Instance_TTS.Speak(GameManager.Instance.dataList.DataSet[wordIndex].Audio);
        player.GetComponent<PlayerMovement>().canMove = false;
        dialogueManager.StartDialogue(dialogue,true);
        isInteractable = false;
    }
}
