using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    #region PRIVATE VARIABLES 

    [SerializeField] List<Image> inputBoxList = new List<Image>();
    [SerializeField] List<Button> buttons = new List<Button>();
    [SerializeField] Image inputBoxPrefab;
    [SerializeField] Transform inputBoxParent;
    [SerializeField] Transform historyPanel;
    [SerializeField] float waitingTime=1f;

    [SerializeField] WordGenderManager wordGenderManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] Animator wordlePanelAnimator;

    [SerializeField] Color correctLetterColor;
    [SerializeField] Color incorrectLetterColor;
    [SerializeField] Color wrongPositionColor;

    private int inputIndex;
    private int correctLetters;
    private string answerText;
    #endregion

    #region PRIVATE FUNCTIONS

    private IEnumerator CorrectAnswerRoutine()
    {
        yield return new WaitForSeconds(waitingTime);
        AddWordToHistoryList();
        wordlePanelAnimator.SetBool("Open", false);
        yield return new WaitForSeconds(0.7f);
        uiManager.OpenMainPanel();
        wordGenderManager.ResetGenderWheel();
        foreach (Transform child in historyPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Button btn in buttons)
        {
            btn.image.color = Color.white;
        }
    }

    private IEnumerator InccorectAnswerRoutine()
    {
        HighLightKeys();
        yield return new WaitForSeconds(waitingTime);
        AddWordToHistoryList();
        wordlePanelAnimator.SetBool("Open", false);
        yield return new WaitForSeconds(0.7f);
        uiManager.OpenMainPanel();
    }

    private void HighLightKeys()
    {
        foreach (Image input in inputBoxList)
        {
            string text = input.GetComponentInChildren<TMP_Text>().text;

            Button button = buttons.Find(x => x.name == text);
            button.image.color = input.color;
        }
    }

    private void AddWordToHistoryList()
    {
        historyPanel.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        historyPanel.GetComponent<GridLayoutGroup>().constraintCount = answerText.Length;
        for (int i = 0; i < inputBoxList.Count; i++)
        {
            Image inputBox = Instantiate(inputBoxPrefab, historyPanel);
            inputBox.GetComponentInChildren<TMP_Text>().text = GetTextFromInputBox(inputBoxList[i]);
            inputBox.GetComponentInChildren<TMP_Text>().color = Color.black;
            inputBox.color = inputBoxList[i].color;
            Destroy(inputBoxList[i].gameObject);
        }
        inputBoxList.Clear();
    }

    private string GetTextFromInputBox(Image inputBox)
    {
        string text = inputBox.GetComponentInChildren<TMP_Text>().text;
        return text;
    }
    #endregion

    #region PUBLIC FUNCTIONS

    public void OnEnable()
    {
        int num = PlayerPrefs.GetInt("words");
       
        answerText = GameManager.Instance.dataList.DataSet[num].SP_Name;
        answerText = answerText.Replace(" ", "");
        for (int i = 0; i < answerText.Length; i++)
        {
            Image inputBox = Instantiate(inputBoxPrefab, inputBoxParent);
            inputBoxList.Add(inputBox);
        }

        
        answerText= answerText.Replace("ó", "o");
        answerText = answerText.Replace("á", "a");
        answerText = answerText.Replace("é", "e");
        answerText = answerText.Replace("í", "i");
        answerText = answerText.Replace("ú", "u");
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
            return;
        }
        for (int i = 0; i < inputBoxList.Count; i++)
        {
            string tmptext = inputBoxList[i].GetComponentInChildren<TMP_Text>().text;
            if (tmptext == answerText[i].ToString())
            {
                inputBoxList[i].color = correctLetterColor;
                correctLetters++;
            }
            else if(answerText.Contains(tmptext))
            {
                inputBoxList[i].color = wrongPositionColor;
            }
            else
            {
                inputBoxList[i].color = incorrectLetterColor;
            }
        }

        if(correctLetters==answerText.Length)
        {
            int num= PlayerPrefs.GetInt("words");      
            PlayerPrefs.SetInt("words", num+1);
            StartCoroutine(CorrectAnswerRoutine());
        }
        else
        {          
            StartCoroutine(InccorectAnswerRoutine());
        }
        inputIndex = 0;
        correctLetters = 0;
    }
    #endregion
}