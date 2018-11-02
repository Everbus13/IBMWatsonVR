using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;
using System;

public class CustomTTS : MonoBehaviour {
    
    string _testString = "no";
    private static string _username = "b5064546-7bc8-4306-aefd-12424c275da2";
    private static string _password = "B172qSzTrvro";
    private static string _serviceUrl = "https://stream.watsonplatform.net/text-to-speech/api";
    // Use this for initialization
    void Start () {
        Credentials credentials = new Credentials(_username, _password, _serviceUrl);
        TextToSpeech _service = new TextToSpeech(credentials);

        _service.Voice = VoiceType.en_US_Allison;
        _service.ToSpeech(HandleToSpeechCallback, OnFail, _testString, false);

    }
    void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData = null)
    {
        GameObject audioObject = new GameObject("AudioObject");
        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.spatialBlend = 0.0f;
        source.loop = false;
        source.clip = clip;
        source.Play();
        Destroy(audioObject, clip.length);
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        throw new NotImplementedException();
    }
}
