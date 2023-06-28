using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text wordText;
    [SerializeField] TMP_Text englishDescription;
    [SerializeField] TMP_Text spanishDescription;

    [SerializeField] GameObject allOptionPanel;
    [SerializeField] GameObject historyPanel;
    [SerializeField] GameObject wordlePanel;
    [SerializeField] GameObject analyzePanel;
    [SerializeField] GameObject genderPanel;

    private void Start()
    {
        wordText.text = GameManager.wordsList.wordData[PlayerPrefs.GetInt("words")].EN_Name;
    }

    public void OpenMainPanel()
    {
        CloseAllPanel();
        allOptionPanel.SetActive(true);
        wordText.text = GameManager.wordsList.wordData[PlayerPrefs.GetInt("words")].EN_Name;
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
        englishDescription.text ="English Definition: " +GameManager.wordsList.wordData[wordNumber].EN_Definition;
        spanishDescription.text = "Spanish Definition: " + GameManager.wordsList.wordData[wordNumber].SP_Definition;
        analyzePanel.SetActive(true);
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
}
