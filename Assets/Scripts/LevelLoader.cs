using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator fadeAnimator;

    private void Start()
    {
        
    }
    public void LoadGame()
    {
        fadeAnimator.SetBool("FadeIn", true);
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}