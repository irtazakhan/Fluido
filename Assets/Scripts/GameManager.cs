using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string[] words = {"Casa","Tienda","Escuela" };

    private void Awake()
    {
        
        int numGameSession = FindObjectsOfType<GameManager>().Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


}
