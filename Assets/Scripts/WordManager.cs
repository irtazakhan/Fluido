using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    [SerializeField] List<Image> inputBoxList = new List<Image>();
    [SerializeField] Button[] Buttons;
    [SerializeField] Image inputBoxPrefab;
    [SerializeField] Transform inputBoxParent;

    private int inputIndex;
    private int correctLetters;
    public string answerText;

    private void Awake()
    {
       
        int num = PlayerPrefs.GetInt("words");
        Debug.Log(num);
        answerText = GameManager.words[num];
        for (int i = 0; i < answerText.Length; i++)
        {
            Image inputBox = Instantiate(inputBoxPrefab, inputBoxParent);
            inputBoxList.Add(inputBox);
        }

        answerText = answerText.ToUpper();
    }

    public void SetLetter(string c)
    {
        if (inputIndex >= inputBoxList.Count) return;

        inputBoxList[inputIndex].GetComponentInChildren<TMP_Text>().text = c.ToUpper();
        inputIndex++;
       
    }

    public void RemoveLetter()
    {
        if (inputIndex <= 0)
            return;
        inputIndex--;
        inputBoxList[inputIndex].GetComponentInChildren<TMP_Text>().text = "";
        
    }

    public void SubmitWord()
    {
        if(inputIndex-1< inputBoxList.Count-1)
        {
            Debug.Log(inputIndex + "Length Error");
            return;
        }
        for (int i = 0; i < inputBoxList.Count; i++)
        {
            string tmptext = inputBoxList[i].GetComponentInChildren<TMP_Text>().text;
            if (tmptext == answerText[i].ToString())
            {
                inputBoxList[i].color = Color.green;
                correctLetters++;
            }
            else if(answerText.Contains(tmptext))
            {
                inputBoxList[i].color = Color.yellow;
            }
            else
            {
                inputBoxList[i].color = Color.red;
            }
        }

        if(correctLetters==answerText.Length)
        {
            Debug.Log("Correct Answer");
            int num= PlayerPrefs.GetInt("words");
            
            PlayerPrefs.SetInt("words", num+1);
            SceneManager.LoadScene("UI Base");
        }
        else
        {
            
            Debug.Log("Incorrect Answer");
            SceneManager.LoadScene("wordle");
        }
        correctLetters = 0;
    }
}
