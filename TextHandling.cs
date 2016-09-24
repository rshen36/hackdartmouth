using System;
using System.Net;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace GoogleAPIRequests {
    class GRequests {
        private string DetectText(string imgPath) {
            /// Google Vision API static values
            string apiKey = "AIzaSyA8Gc4eafOjnhnj5QYn7I2ZNEumpxtxY-s";
            string visionURL = "https://vision.googleapis.com/v1/images:annotate?key=" + apiKey;

            string result = "";
            using (var client = new WebClient()) {
                string jsonRequest = "";
                using (Image img = Image.FromFile(imgPath)) {
                    using (MemoryStream m = new MemoryStream()) {
                        img.Save(m, img.RawFormat);
                        byte[] imgBytes = m.ToArray();

                        string imgBase64 = Convert.ToBase64String(imgBytes);

                        /// currently only utilizes the text detection feature
                        jsonRequest = "{ \"requests\":[ { \"image\":{ \"content\": \"" + imgBase64 +
                            "\"}, \"features\":[ { \"type\":\"TEXT_DETECTION\", \"maxResults\":10 } ] } ] }";
                    }
                }

                try {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    result = client.UploadString(visionURL, "POST", jsonRequest);

                    // check if text found (caught exception if not)
                    // TODO: improve this error handling
                    dynamic resObj = JsonConvert.DeserializeObject(result);
                    dynamic check = resObj.responses[0].textAnnotations;
                }
                catch (Exception ex) {
                    // TODO: properly handle exceptions (how do we want to handle?)
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }

            return result;
        }

        private string TranslateText(string res, string targetLang) {
            // TODO: improve error handling from current of just returning empty string (?)

            /// Google Translate API static values
            string apiKey = "AIzaSyA8Gc4eafOjnhnj5QYn7I2ZNEumpxtxY-s";
            string translateURL = "https://www.googleapis.com/language/translate/v2?key=" + apiKey;

            string query = "&q=";
            string source = "&source=";
            string target = "&target=" + targetLang;

            dynamic textDetections = JsonConvert.DeserializeObject(res);
            string transResponse = "";
            try {
                textDetections = textDetections.responses[0].textAnnotations;
                source = source + textDetections[0].locale;

                // return empty string if target language is same as source language
                if (targetLang.Equals(textDetections[0].locale, StringComparison.Ordinal)) {
                    return transResponse;
                }

                for (int i = 1; i < textDetections.Count; i++) {
                    query = query + textDetections[i].description + "+";
                }
                query = query.Substring(0,query.Length - 1);
                Console.WriteLine(query);

                translateURL = translateURL + query + source + target;
                Console.WriteLine(translateURL);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(translateURL);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream respStream = response.GetResponseStream())
                using (StreamReader respReader = new StreamReader(respStream)) {
                    transResponse = respReader.ReadToEnd();
                }
            }
            catch (Exception ex) {
                // TODO: properly handle exceptions (how do we want to handle?)
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            return transResponse;
        }
    }
}