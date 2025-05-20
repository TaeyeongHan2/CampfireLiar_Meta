using UnityEngine;

//서버로 전송해주는 클래스
using System.Collections;
using System.IO;
using HTTP;
using UnityEngine;
using UnityEngine.Networking;

public class AudioResult : ResultBase
{
    public string result_text;
}
public class VoiceSender : MonoBehaviour
{
    // public VoiceRecorder recorder;
    public string serverURL = "http://192.168.0.23:8080/upload-audio-url"; // <- API 주소 대입
    public string result_text;
    public void SendAudio(System.Action<string> callback)
    {
        // AudioClip clip = recorder.GetClip();
        // byte[] wavData = WavUtility.FromAudioClip(clip);
        
        string filePath = Path.Combine(Application.dataPath, "Resources/sample.wav");;
        byte[] fileData = File.ReadAllBytes(filePath);
        StartCoroutine(SendToServer(fileData, callback));
    }

    IEnumerator SendToServer(byte[] wavBytes, System.Action<string> onComplete)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("voiceFile", wavBytes, "sample.wav", "audio/wav");

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var result = ApiBase.GetResultFromJson<AudioResult>(www);
                onComplete?.Invoke(result.result_text);
                Debug.Log("✅ 서버 전송 성공!");
            }
            else
            {
                onComplete?.Invoke("에러");
                Debug.LogError("❌ 서버 전송 실패: " + www.error);
            }
        }
    }
}

