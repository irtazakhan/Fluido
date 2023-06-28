using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text wordText;
    [SerializeField] GameObject allOptionPanel;
    [SerializeField] GameObject historyPanel;
    [SerializeField] GameObject wordlePanel;
    [SerializeField] GameObject analyzePanel;
    [SerializeField] GameObject genderPanel;

    private void Start()
    {
        wordText.text = GameManager.englishWords[PlayerPrefs.GetInt("words")];
    }

    public void OpenMainPanel()
    {
        CloseAllPanel();
        allOptionPanel.SetActive(true);
        wordText.text = GameManager.englishWords[PlayerPrefs.GetInt("words")];
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
