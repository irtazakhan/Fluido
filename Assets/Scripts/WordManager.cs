using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordManager : MonoBehaviour
{
    [SerializeField] Image[] inputBox;
    [SerializeField] Button[] Buttons;


    private int inputIndex;

    public string answerText = "CASA";

    public void SetLetter(string c)
    {
        if (inputIndex >= inputBox.Length) return;

        inputBox[inputIndex].GetComponentInChildren<TMP_Text>().text = c.ToUpper();
        inputIndex++;
       
    }

    public void RemoveLetter()
    {
        if (inputIndex <= 0)
            return;
        inputIndex--;
        inputBox[inputIndex].GetComponentInChildren<TMP_Text>().text = "";
        
    }

    public void SubmitWord()
    {
        if(inputIndex-1<inputBox.Length-1)
        {
            Debug.Log(inputIndex + "Length Error");
            return;
        }
        for (int i = 0; i < inputBox.Length; i++)
        {
            string tmptext = inputBox[i].GetComponentInChildren<TMP_Text>().text;
            if (tmptext == answerText[i].ToString())
            {
                inputBox[i].color = Color.green;
            }
            else if(answerText.Contains(tmptext))
            {
                inputBox[i].color = Color.yellow;
            }
            else
            {
                inputBox[i].color = Color.red;
            }
        }
    }
}
