using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
    //public static WordsList wordsList;
    public DataList dataList;
    public static GameManager Instance;
    [HideInInspector] public UIManager Instance_UI;
    [HideInInspector] public TextToSpeech Instance_TTS;
    #endregion

    #region PRIVATE FUNCTIONS

    private void Awake()
    {
        Instance_UI = FindAnyObjectByType<UIManager>();
        Instance_TTS = FindAnyObjectByType<TextToSpeech>();
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
}