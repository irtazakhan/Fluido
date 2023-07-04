using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region PRIVATE VARIABLES 
    [SerializeField] TMP_Text wordText;
    [SerializeField] TMP_Text englishDescription;
    [SerializeField] TMP_Text spanishDescription;
    [SerializeField] GameObject allOptionPanel;
    [SerializeField] GameObject historyPanel;
    [SerializeField] GameObject wordlePanel;
    [SerializeField] GameObject analyzePanel;
    [SerializeField] GameObject genderPanel;
    #endregion


    #region PRIVATE FUNCTIONS
    private void Start()
    {
        wordText.text = GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].EN_Name;
    }
    #endregion

    #region PUBLIC FUNCTIONS

    public void OpenMainPanel()
    {
        CloseAllPanel();
        allOptionPanel.SetActive(true);
        wordText.text = GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].EN_Name;
    }

    public void OpenWordlePanel()
    {
        CloseAllPanel();
        wordlePanel.SetActive(true);
    }

    public void OpenHistoryPanel()
    {
        historyPanel.SetActive(true);
    }

    public void OpenAnalyzePanel()
    {
        int wordNumber = PlayerPrefs.GetInt("words");
        englishDescription.text ="English Definition: " + GameManager.Instance.dataList.DataSet[wordNumber].EN_Definition;
        spanishDescription.text = "Spanish Definition: " + GameManager.Instance.dataList.DataSet[wordNumber].SP_Definition;
        analyzePanel.SetActive(true);
    }

    public void PlayAudio()
    {
        GameManager.Instance.Instance_TTS.Speak(GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].Audio);
    }
    public void OpenGenderPanel()
    {
        CloseAllPanel();
        genderPanel.SetActive(true);
    }

    public void CloseAllPanel()
    {
        allOptionPanel.SetActive(false);
        historyPanel.SetActive(false);
        wordlePanel.SetActive(false);
        analyzePanel.SetActive(false);
        genderPanel.SetActive(false);
    }
    #endregion
}
