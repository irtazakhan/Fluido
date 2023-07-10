using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator fadeAnimator;
    [SerializeField] TMP_Text startText;
    [SerializeField] float blinkTime = 0.5f;
    [SerializeField] float blinkRate = 0.07f;

    float time = 0;
    private void Start()
    {
        //time = 0;
    }
    public void LoadGame()
    {
        SoundManager.ins.PlaySfx("Atacar");
        StartCoroutine(ColorChangeRoutine());
    }

    IEnumerator ColorChangeRoutine()
    {
        while (time < blinkTime)
        {
            Color textColor = startText.color;
            time += 0.05f;
            if (textColor.a == 0)
            {
                startText.color = new Color(textColor.r, textColor.g, textColor.b, 1);
            }
            else
            {
                startText.color = new Color(textColor.r, textColor.g, textColor.b, 0);
            }
            yield return new WaitForSeconds(blinkRate);
        }
        yield return new WaitForSeconds(0.2f);
        fadeAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
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