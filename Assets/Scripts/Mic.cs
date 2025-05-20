using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mic : MonoBehaviour
{
    AudioClip record;
    public VoiceSender _voiceSender;
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        // RecSnd();
        // Invoke("PlaySnd", 6f);
    }

    public void PlaySnd()
    {
        aud.Play();
        SavWav.Save("C:\\Users\\User\\Desktop\\MidNight\\Assets\\Resources\\sample", aud.clip); // 저장 기능, Test라는 이름으로 저장된다
        _voiceSender.SendAudio((string resultText) =>
        {
            float similarity = TextSimilarity.CalculateSimilarity(resultText, ResultData.answer);
            float similaritya = similarity * 100f;
            if (similaritya > 50f) 
                Single.System.SceneManager.LoadSceneResult(SceneDataType.GameResult, true, resultText, similaritya);
            else
                Single.System.SceneManager.LoadSceneResult(SceneDataType.GameResult, false, resultText, similaritya);
        });
    }

    public void RecSnd()
    {
        // 디바이스 확인용 코드로, 생략해도 된다
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        record = Microphone.Start(Microphone.devices[0].ToString(), false, 3, 44100); // 3초 녹음
        aud.clip = record;

    }
}