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
    public static UIManager Instance_UI;
    #endregion

    #region PRIVATE FUNCTIONS

    private void Awake()
    {
        Instance_UI = FindAnyObjectByType<UIManager>();

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