using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

public class GoogleSheetDataFetcher : MonoBehaviour
{
    private const string GoogleSheetURL = "https://docs.google.com/spreadsheets/d/1PwqQsklNJR2kxMjaWBvnrOf0wm8k0izeN5RnvhnoD1w/export?format=csv";
    private DataList dataList;

    private void Start()
    {
        StartCoroutine(GetSheetData());
    }

    private IEnumerator GetSheetData()
    {
        dataList = GameManager.Instance.dataList;
        using (UnityWebRequest www = UnityWebRequest.Get(GoogleSheetURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string csvData = www.downloadHandler.text;
                ConvertCsvToDataList(csvData);
            }
            else
            {
                Debug.LogError("Failed to retrieve data from Google Sheet: " + www.error);
            }
        }
    }

    private void ConvertCsvToDataList(string csvData)
    {
        List<Data> dataList = new List<Data>();

        string[] lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++) // Start from index 1 to skip the header line
        {
            string[] values = SplitCsvLine(lines[i]);

            Data data = new Data();

            data.No = Convert.ToInt32(values[0]);
            data.EN_Name = values[1];
            data.Gender = values[2];
            data.SP_Name = values[3];
            data.Length = Convert.ToInt32(values[4]);
            data.Type = values[5];
            data.Priority = Convert.ToInt32(values[6]);
            data.EN_Definition = values[7];
            data.SP_Definition = values[8];
            data.Sprite = values[9];
            data.Audio = values[10];

            dataList.Add(data);
        }

        this.dataList.DataSet = dataList.ToArray();
    }

    private string[] SplitCsvLine(string line)
    {
        List<string> values = new List<string>();
        StringBuilder currentValue = new StringBuilder();
        bool withinQuotes = false;

        foreach (char c in line)
        {
            if (c == '"')
            {
                withinQuotes = !withinQuotes;
            }
            else if (c == ',' && !withinQuotes)
            {
                values.Add(currentValue.ToString().Trim());
                currentValue.Clear();
            }
            else
            {
                currentValue.Append(c);
            }
        }

        values.Add(currentValue.ToString().Trim());
        return values.ToArray();
    }
}
