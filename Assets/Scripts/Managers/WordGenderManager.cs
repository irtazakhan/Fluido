using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGenderManager : MonoBehaviour
{
    #region PRIVATE VARIABLES 

    [SerializeField] Image genderWheelImage;
    [SerializeField] Button maleArticleButton;
    [SerializeField] Button femaleArticleButton;
    [SerializeField] UIManager uiManager;
    [SerializeField] Animator genderPanelAnimator;
    #endregion

    #region PRIVATE FUNCTIONS

    private void OnEnable()
    {
        int num = PlayerPrefs.GetInt("words");
        string gender = GameManager.Instance.dataList.DataSet[num].Gender;

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
        }else
        {
            OpenWordlepanel();
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
        genderPanelAnimator.SetBool("Open", false);
        StartCoroutine(GenderPanelCloseRoutine());
    }

    IEnumerator GenderPanelCloseRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        uiManager.OpenWordlePanel();
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public void ResetGenderWheel()
    {
        genderWheelImage.fillAmount = 0;
        maleArticleButton.onClick.RemoveAllListeners();
        femaleArticleButton.onClick.RemoveAllListeners();
        maleArticleButton.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
        femaleArticleButton.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
    }
    #endregion
}