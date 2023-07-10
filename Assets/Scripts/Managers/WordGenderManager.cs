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
    [SerializeField] float gapBetweenSounds=0.3f;
    [SerializeField] Image[] wheelImages;
    [SerializeField] int shakeFactor=2;

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
        SoundManager.ins.PlaySfx("Atacar");
        
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
        SoundManager.ins.PlaySfx("Atacar");
        StartCoroutine(GenderInccorectRoutine());
    }

    IEnumerator GenderInccorectRoutine()
    {
        yield return new WaitForSeconds(gapBetweenSounds);
        SoundManager.ins.PlaySfx("Gender Miss");
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
        yield return new WaitForSeconds(gapBetweenSounds);
        uiManager.OpenWordlePanel();
    }


    private IEnumerator Shake()
    {
        
        float time = 0;
        int wheelNum = uiManager.genderCorrectTimes;

        yield return new WaitForSeconds(0.3f);
        SoundManager.ins.PlaySfx("Gender Correct");

        if (wheelNum<3)
        {
            float directionX=1;
            float directionY=1;
            while (time < 1)
            {
                directionX = Random.Range( -1f , 1f);
                directionY = Random.Range(-1f, 1f);
                genderWheelImage.position =startPos+ new Vector3(directionX*shakeFactor,directionY*shakeFactor);
               
                if (wheelImages[wheelNum].rectTransform.localScale.x >= 1)
                {
                    wheelImages[wheelNum].rectTransform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    wheelImages[wheelNum].rectTransform.localScale += new Vector3(0.055f, 0.055f, 0.055f);
                }

                yield return new WaitForSeconds(Time.deltaTime);
                time += Time.deltaTime;
            }

            genderWheelImage.position = startPos;
        }
        
       // genderWheelImage.position = startPos;
        yield return new WaitForSeconds(0.5f);
        OpenWordlepanel();
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public void ResetGenderWheel()
    {
        //genderWheelImage.fillAmount = 0;

        uiManager.genderCorrectTimes = 0;
        for (int i = 0; i < wheelImages.Length; i++)
        {
            wheelImages[i].transform.localScale = Vector3.zero;
        }

        maleArticleButton.onClick.RemoveAllListeners();
        femaleArticleButton.onClick.RemoveAllListeners();
        maleArticleButton.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
        femaleArticleButton.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
    }
    #endregion
}