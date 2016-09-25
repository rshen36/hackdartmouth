using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class TextHandler : MonoBehaviour {
    private string DetectText(string imgPath)
    {
        /// Google Vision API static values
        string apiKey = "AIzaSyA8Gc4eafOjnhnj5QYn7I2ZNEumpxtxY-s";
        string visionURL = "https://vision.googleapis.com/v1/images:annotate?key=" + apiKey;

        string result = "";
        string jsonRequest = "";

        byte[] imgBytes = System.IO.File.ReadAllBytes(@imgPath);
        string imgBase64 = Convert.ToBase64String(imgBytes);
        var encoding = new System.Text.UTF8Encoding();

        /// currently only utilizes the text detection feature
        jsonRequest = "{ \"requests\":[ { \"image\":{ \"content\": \"" + imgBase64 +
            "\"}, \"features\":[ { \"type\":\"TEXT_DETECTION\", \"maxResults\":10 } ] } ] }";
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        WWW www = new WWW(visionURL, encoding.GetBytes(jsonRequest), headers);
        StartCoroutine(WaitForRequest(www));
        if (www.error == null)
        {
            result = www.text;
        }
        else
        {
            Debug.LogError(www.error);
        }

        return result;
    }

    private string TranslateText(string res, string targetLang)
    {
        // TODO: improve error handling from current of just returning empty string (?)

        /// Google Translate API static values
        string apiKey = "AIzaSyA8Gc4eafOjnhnj5QYn7I2ZNEumpxtxY-s";
        string translateURL = "https://www.googleapis.com/language/translate/v2?key=" + apiKey;

        string query = "&q=";
        string source = "&source=";
        string target = "&target=" + targetLang;

        JsonData textDetections = JsonMapper.ToObject(res);
        string transResponse = "";
        JsonData responses = textDetections["responses"][0]["textAnnotations"];
        source = source + responses[0]["locale"].ToString();

        // TODO: ensure that source language and target language are different

        for (int i = 1; i < responses.Count; i++)
        {
            query = query + responses[i]["description"].ToString() + "+";
        }
        query = query.Substring(0, query.Length - 1);

        translateURL = translateURL + query + source + target;
        WWW www = new WWW(translateURL);
        StartCoroutine(WaitForRequest(www));
        if (www.error == null)
        {
            transResponse = www.text;
        }
        else
        {
            Debug.LogError(www.error);
        }

        return transResponse;
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
    }

    static void Main(string[] args)
    {
        string imgPath = "C:\\Users\\Rich\\Downloads\\keep-calm-and-carry-on-17042.png";
        TextHandler t = new TextHandler();
        string res = t.DetectText(imgPath);
        string translations = t.TranslateText(res, "de");
        Debug.Log(translations);
    }
}
