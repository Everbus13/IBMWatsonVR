using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using System.IO;
using System;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;

public class CustomSTT : MonoBehaviour {
    //SpeechToText Authentication
    private static string _username = "95c2bcf5-dab1-47c1-85e7-f5a4b7395bb7";
    private static string _password = "HMqvmaWwWdLx";
    private static string _serviceUrl = "https://stream.watsonplatform.net/speech-to-text/api";
    static Credentials credentials = new Credentials(_username, _password, _serviceUrl);
    SpeechToText speechToText = new SpeechToText(credentials);

    // Use this for initialization
    void Start () {
        //Get Resource
        string resourcePath = Application.dataPath + "/sound/1.mp3";
        byte[] resource = File.ReadAllBytes(resourcePath);
        string resourceType = Utility.GetMimeType(Path.GetExtension(resourcePath));

        //Create keyword list
        List<string> keywords = new List<string>();
        keywords.Add("speech");
        speechToText.KeywordsThreshold = 0.5f;
        speechToText.InactivityTimeout = 120;
        speechToText.StreamMultipart = false;
        speechToText.Keywords = keywords.ToArray();

        Debug.Log("Attempting to recognize", gameObject);
        //Recognize
        speechToText.Recognize(HandleOnRecognize, OnFail, resource, resourceType);
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        throw new NotImplementedException();
    }

    private void HandleOnRecognize(SpeechRecognitionEvent response, Dictionary<string, object> customData){
        //Print Response
        Debug.Log(customData["json"].ToString(), gameObject);
        Debug.Log("Done", gameObject);
    }
}
