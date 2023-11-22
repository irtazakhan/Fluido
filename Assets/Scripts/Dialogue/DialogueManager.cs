using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public TMP_Text nameText;
	public TMP_Text dialogueText;

	private Queue<string> sentences;

	public static bool isDialogueEnded = false;

	public bool isSentenceEnded = false;

	private string currentSentence;
	private Coroutine typeSentenceRoutine;

	void Start()
	{
		sentences = new Queue<string>();
		isDialogueEnded = false;
	}

	private void Update()
	{
		if (sentences != null)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				DisplayNextSentence();
			}
		}
	}

	public void StartDialogue(Dialogue dialogue)
	{

		isDialogueEnded = false;
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(isSentenceEnded)
        {
			if (sentences.Count == 0)
			{
				EndDialogue();
				return;
			}

			currentSentence = sentences.Dequeue();
			StopAllCoroutines();

			typeSentenceRoutine=StartCoroutine(TypeSentence(currentSentence));
		}
		
		else
        {
			StopCoroutine(typeSentenceRoutine);
			dialogueText.text = currentSentence;
			isSentenceEnded = true;
		}
		
	}



	IEnumerator TypeSentence(string sentence)
	{
		isSentenceEnded = false;
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{			
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.5f);
		}
		isSentenceEnded = true;
	}

	void EndDialogue()
	{

	}
}
