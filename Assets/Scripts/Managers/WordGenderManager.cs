using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGenderManager : MonoBehaviour
{
    #region PRIVATE VARIABLES 

    [SerializeField] RectTransform genderWheelImage;
    [SerializeField] Button maleArticleButton;
    [SerializeField] Button femaleArticleButton;
    [SerializeField] UIManager uiManager;
    [SerializeField] Animator genderPanelAnimator;

    [SerializeField] Image[] wheelImages;

    private Vector3 startPos;

    #endregion

    #region PRIVATE FUNCTIONS

    private void OnEnable()
    {
        int num = PlayerPrefs.GetInt("words");
        
        string gender = GameManager.Instance.dataList.DataSet[num].Gender;
        femaleArticleButton.interactable = true;
        maleArticleButton.interactable = true;
        maleArticleButton.onClick.RemoveAllListeners();
        femaleArticleButton.onClick.RemoveAllListeners();
        
        if (gender == "el")
        {
            maleArticleButton.onClick.AddListener(() =>
            {
                CorrectOption(maleArticleButton.GetComponentInChildren<TMP_Text>());   
            });
            femaleArticleButton.onClick.AddListener(() => IncorrectOption());
        }
        else if (gender == "la")
        {
            femaleArticleButton.onClick.AddListener(() =>
            {
                CorrectOption(femaleArticleButton.GetComponentInChildren<TMP_Text>());
            });
            maleArticleButton.onClick.AddListener(() => IncorrectOption());
        }
        else
        {
            OpenWordlepanel();
        }

        startPos = genderWheelImage.position;
    }

    private void CorrectOption(TMP_Text genderText)
    {
        StartCoroutine(Shake());
        int genderCorrect = uiManager.genderCorrectTimes;
        uiManager.genderCorrectTimes += 1;
        
        if(genderCorrect>=3)
        {
            genderText.fontStyle = FontStyles.Bold;
        }

        femaleArticleButton.interactable = false;
        maleArticleButton.interactable = false;
    }

    private void IncorrectOption()
    {
        femaleArticleButton.interactable = false;
        maleArticleButton.interactable = false;
        OpenWordlepanel();
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


    private IEnumerator Shake()
    {
        float time = 0;
        int wheelNum = uiManager.genderCorrectTimes;

        if(wheelNum<3)
        {
            while (time < 1)
            {
                genderWheelImage.position += new Vector3(Mathf.Sin(Time.time * 40) * 0.5f, Mathf.Sin(Time.time * 40) * 0.5f);
               
                if (wheelImages[wheelNum].rectTransform.localScale.x <= 0)
                {
                    wheelImages[wheelNum].rectTransform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    wheelImages[wheelNum].rectTransform.localScale -= new Vector3(0.004f, 0.004f, 0.004f);
                }

                yield return new WaitForSeconds(Time.deltaTime);
                time += Time.deltaTime;
            }
        }

        genderWheelImage.position = startPos;
        yield return new WaitForSeconds(0.5f);
        OpenWordlepanel();
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public void ResetGenderWheel()
    {
        //genderWheelImage.fillAmount = 0;
        maleArticleButton.onClick.RemoveAllListeners();
        femaleArticleButton.onClick.RemoveAllListeners();
        maleArticleButton.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
        femaleArticleButton.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
    }
    #endregion
}