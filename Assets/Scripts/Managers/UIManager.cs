using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

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
    [SerializeField] GameObject phoenPanel;
    [SerializeField] GameObject genderImageObject;
    [SerializeField] Animator historyPanelAnimator;
    [SerializeField] Animator analisePanelAnimator;
    [SerializeField] Animator genderPanelAnimator;
    [SerializeField] Animator wordlePanelAnimator;
    [SerializeField] Animator phonePanelAnimator;

    [SerializeField] Image wordPicture;
    [SerializeField] Sprite blank;
    #endregion

    #region PUBLIC VARIABLES
    public int genderCorrectTimes=0;
    #endregion

    #region PRIVATE FUNCTIONS
    private void Start()
    {
        SoundManager.ins.PlayMusic("Battle");
        wordText.text = GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].EN_Name;

        //Sprite wordSprite= Resources.Load<Sprite>("Sprites/" + GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].Sprite);
       //if (wordSprite != null)
        {
           // wordPicture.sprite = wordSprite;
        }
       // else
        {
           // wordPicture.sprite = blank;
        }       
    }
    #endregion

    #region PUBLIC FUNCTIONS

    public void OpenMainPanel()
    {
        CloseAllPanel();
        allOptionPanel.SetActive(true);

        wordText.text = GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].EN_Name;
       
        //Sprite wordSprite = Resources.Load<Sprite>("Sprites/" + GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].Sprite);
       // if (wordSprite != null)
        {
            //wordPicture.sprite = wordSprite;
        }
       // else
        {
           // wordPicture.sprite = blank;
        }
    }

    public void OpenWordlePanel()
    {
        CloseAllPanel();
        wordlePanel.SetActive(true);
        wordlePanelAnimator.SetBool("Open", true);
    }

    public void OpenHistoryPanel()
    {
        SoundManager.ins.PlaySfx("Open");
        historyPanel.SetActive(true);
        historyPanelAnimator.SetBool("Open", true);
    }

    public void CloseHistoryPanel()
    {
        SoundManager.ins.PlaySfx("Close");
        historyPanelAnimator.SetBool("Open", false);
        StartCoroutine(CloseHistoryPanelRoutine());
    }

    IEnumerator CloseHistoryPanelRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        historyPanel.SetActive(false);
    }

    public void OpenAnalyzePanel()
    {
        SoundManager.ins.PlaySfx("Open");
        int wordNumber = PlayerPrefs.GetInt("words");
        englishDescription.text ="English Definition: " + GameManager.Instance.dataList.DataSet[wordNumber].EN_Definition;
        spanishDescription.text = "Spanish Definition: " + GameManager.Instance.dataList.DataSet[wordNumber].SP_Definition;
        analyzePanel.SetActive(true);
        analisePanelAnimator.SetBool("Open", true);
    }

    public void CloseAnalyzePanel()
    {
        SoundManager.ins.PlaySfx("Close");
        analisePanelAnimator.SetBool("Open", false);
        StartCoroutine(CloseAnalyzePanelRoutine());
    }

    public void OpenPhonePanel()
    {
        phoenPanel.SetActive(true);
        phonePanelAnimator.SetBool("IsOpen", true);
    }

    public void ClosePhonePanel()
    {
        phonePanelAnimator.SetBool("IsOpen", false);
        StartCoroutine(ClosePhonePanelRoutine());
    }

    IEnumerator ClosePhonePanelRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        phoenPanel.SetActive(false);
    }

    IEnumerator CloseAnalyzePanelRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        analyzePanel.SetActive(false);
    }

    public void PlayAudio()
    {
        SoundManager.ins.PauseAndResumeMusic(3);
        GameManager.Instance.Instance_TTS.Speak(GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].Audio);
    }
    public void OpenGenderPanel()
    {
        SoundManager.ins.PlaySfx("Atacar");
        if(GameManager.Instance.dataList.DataSet[PlayerPrefs.GetInt("words")].Gender==string.Empty)
        {
            genderImageObject.SetActive(false);
            OpenWordlePanel();
        }
        else
        {
            CloseAllPanel();
            genderImageObject.SetActive(true);
            genderPanel.SetActive(true);
            genderPanelAnimator.SetBool("Open", true);
        } 
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