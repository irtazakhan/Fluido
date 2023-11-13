using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIController : MonoBehaviour
{
    [SerializeField] TMP_Text messageText;
    [SerializeField] string message;

    private void Start()
    {
        messageText.text = message;
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact(true);
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
        messageText.gameObject.SetActive(show);
    }
}
