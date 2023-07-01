using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TextToSpeech : MonoBehaviour
{
    private const string TTS_URL = "https://api.voicerss.org/";

    private const string apiKey = "dd857ab0fd534ac7b49ddf84bbc47128"; // Replace with your API key from VoiceRSS
    private string lang_Eng = "en-us"; // Language code: en-us for English,
    private string lang_Spa = "es-es"; // Language code: es-es for Spanish
    public float pitch = 0.5f; // Speech pitch (0.0 to 1.0)
    public float rate = -10f; // Speech rate (0.0 to 1.0)

    public void Speak(string text)
    {
        StartCoroutine(DownloadSpeech(text));
    }

    private IEnumerator DownloadSpeech(string text)
    {
        string url = $"{TTS_URL}?key={apiKey}&hl={lang_Spa}&c=MP3&f=16khz_16bit_stereo&src={UnityWebRequest.EscapeURL(text)}&r={rate}&v={pitch}";

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Text-to-Speech request failed: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
