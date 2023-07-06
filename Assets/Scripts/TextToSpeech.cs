using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TextToSpeech : MonoBehaviour
{
    private const string TTS_URL = "https://api.voicerss.org/";

    private const string apiKey = "dd857ab0fd534ac7b49ddf84bbc47128"; // Replace with your API key from VoiceRSS
  //  private string lang_Eng = "en-us";  // Language code: en-us for English,
  //  private string lang_Spa = "es-es"; // Language code: es-es for Spanish
  //  private string voice = ""; // Voice Speaker Name
    [Range(-10f, 10f)] public float rate = 0f; // Speech rate (-10.0 to 10.0)
    public SpanishAccent selectedAccent;
    public MexicoVoice voiceMexico;
    public SpainVoice voiceSpain;
    public enum SpanishAccent
    {
        Spain,
        Mexico
    }
    public enum MexicoVoice
    {
        Juana,
        Silvia,
        Teresa,
        Jose
    }

    public enum SpainVoice
    {
        Camila,
        Sofia,
        Luna,
        Diego
    }

    public void Speak(string text)
    {
        StartCoroutine(DownloadSpeech(text));
    }

    private IEnumerator DownloadSpeech(string text)
    {
        string languageCode = GetLanguageCode(selectedAccent);
        string voice = (selectedAccent == SpanishAccent.Mexico) ? voiceMexico.ToString() : voiceSpain.ToString();
        string url = $"{TTS_URL}?key={apiKey}&hl={languageCode}&c=MP3&f=16khz_16bit_stereo&src={UnityWebRequest.EscapeURL(text)}&r={rate}&v={voice}";

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
    private string GetLanguageCode(SpanishAccent _Accent)
    {
        switch (_Accent)
        {
            case SpanishAccent.Mexico:
                return "es-mx"; // English language code
            case SpanishAccent.Spain:
                return "es-es"; // Spanish language code
            default:
                return "es-es"; // Default language code
        }
    }
}
