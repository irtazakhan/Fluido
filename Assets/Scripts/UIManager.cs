using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text wordText;

    private void Start()
    {
        
        wordText.text = GameManager.words[PlayerPrefs.GetInt("words")];
    }

    public void LoadWordleScene()
    {
        SceneManager.LoadScene("wordle");
    }
}
