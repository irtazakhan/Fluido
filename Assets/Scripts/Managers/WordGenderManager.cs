using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGenderManager : MonoBehaviour
{
    [SerializeField] Image genderWheelImage;

    private void InccorrectOption()
    {
        
    }

    public void CorrectOption()
    {
        genderWheelImage.fillAmount += 1f / 3f;
    }
}
