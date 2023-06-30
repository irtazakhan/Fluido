using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGenderManager : MonoBehaviour
{
    [SerializeField] Image genderWheelImage;
    [SerializeField] Button maleArticleButton;
    [SerializeField] Button femaleArticleButton;
    [SerializeField] UIManager uiManager;

    private void OnEnable()
    {
        int num = PlayerPrefs.GetInt("words");
        string gender = GameManager.wordsList.wordData[num].Gender;

        maleArticleButton.onClick.RemoveAllListeners();
        femaleArticleButton.onClick.RemoveAllListeners();
        
        if (gender == "el")
        {
            maleArticleButton.onClick.AddListener(() =>
            { 
                CorrectOption(maleArticleButton.GetComponentInChildren<TMP_Text>());
                OpenWordlepanel();
            });
            femaleArticleButton.onClick.AddListener(() => OpenWordlepanel());
        }
        else if (gender == "la")
        {
            femaleArticleButton.onClick.AddListener(() =>
            {
                CorrectOption(femaleArticleButton.GetComponentInChildren<TMP_Text>());
                OpenWordlepanel();
            });
            maleArticleButton.onClick.AddListener(() => OpenWordlepanel());
        }
    }

    private void CorrectOption(TMP_Text genderText)
    {
        genderWheelImage.fillAmount += 1f / 3f;
        if (genderWheelImage.fillAmount >= 1)
        {
            genderText.fontStyle = FontStyles.Bold;
        }
    }

    private void OpenWordlepanel()
    {
        uiManager.OpenWordlePanel();
    }
}