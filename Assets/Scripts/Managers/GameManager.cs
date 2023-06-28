using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string[] spanishWords = {"Casa","Tienda","Escuela","Yo","Lugar","Hospital","Calle","Perro","Numero","Dinero"};
    public static string[] englishWords = { "House","Store","School","I","Place","Hospital","Street","Dog","Number","Money"};

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

public class Word
{
    public string englishName;
    public string spanishName;
    public string gender;
    public string englishDescription;
    public string spanishDescription;
    public string fileImageName;
    public string audioFileName;
}