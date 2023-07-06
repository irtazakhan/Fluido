using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList", menuName = "Data/DataList")]
public class DataList : ScriptableObject
{
    public Data[] DataSet;
}

[System.Serializable]
public class Data
{
    public int No;
    public string EN_Name;
    public string Gender;
    public string SP_Name;
    public int Length;
    public string Type;
    public int Priority;
    public string EN_Definition;
    public string SP_Definition;
    public string Sprite;
    public string Audio;
}

