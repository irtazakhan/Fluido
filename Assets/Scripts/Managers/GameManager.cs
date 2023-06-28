using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static string[] spanishWords = {"Casa","Tienda","Escuela"};
    public static string[] englishWords = { "House","Store","School"};

    public static WordsList wordsList;

    private void Awake()
    {
        if (File.Exists(Application.dataPath + "/words.txt"))
        {
            string json = File.ReadAllText(Application.dataPath + "/words.txt");
            wordsList = JsonUtility.FromJson<WordsList>(json);
        }

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

[System.Serializable]
public class WordsList
{
    public List<WordData> wordData = new List<WordData>();
}

[System.Serializable]
public class WordData
{
    public int No;
    public string EN_Name;
    public string Gender;
    public string SP_Name;
    public int Length;
    public string Type;
    public string EN_Definition;
    public string SP_Definition;
    public string Sprite;
    public string Audio;
}