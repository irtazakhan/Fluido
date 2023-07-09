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
        SoundManager.ins.PlaySfx("Wordle Complete");
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
        
        if(answerText.Length<8)
        {
            historyPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(55, 55);
            historyPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(25, 25);
        }
        else if (answerText.Length >= 8 && answerText.Length<10)
        {
            historyPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(45, 45);
            historyPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 20);
        }
        else if (answerText.Length >= 10)
        {
            historyPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(35, 35);
            historyPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(17, 17);
        }

        for (int i = 0; i < inputBoxList.Count; i++)
        {
            Image inputBox = Instantiate(inputBoxPrefab, historyPanel);
            inputBox.GetComponentInChildren<TMP_Text>().text = GetTextFromInputBox(inputBoxList[i]);       
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

        if(answerText.Length<10)
        {
            inputBoxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(48, 48);
        }
        else if(answerText.Length>=10)
        {
            inputBoxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(35, 35);
        }

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
        int correctLetter = 0;
        if(inputIndex-1< inputBoxList.Count-1)
        {
            return;
        }
        for (int i = 0; i < inputBoxList.Count; i++)
        {
            string tmptext = inputBoxList[i].GetComponentInChildren<TMP_Text>().text;
            if (tmptext == answerText[i].ToString())
            {
                correctLetter++;
                inputBoxList[i].color = correctLetterColor;
                
            }
            else if(answerText.Contains(tmptext))
            {
                inputBoxList[i].color = wrongPositionColor;
            }
            else
            {
                inputBoxList[i].color = incorrectLetterColor;
            }

            if(correctLetter<answerText.Length && correctLetter>0)
            {
                SoundManager.ins.PlaySfx("Wordle Correct");
            }
            else if(correctLetter==0)
            {
                SoundManager.ins.PlaySfx("Gender Miss");
            }
        }

        if(correctLetter == answerText.Length)
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
        
    }
    #endregion
}