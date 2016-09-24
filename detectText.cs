using System;
using System.Net;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace GVisionRequest
{
    class VisionAnnotateRequest
    {
        private string DetectText(string imgPath)
        {
            /// Google Vision API static values
            string apiKey = "AIzaSyA8Gc4eafOjnhnj5QYn7I2ZNEumpxtxY-s";
            string visionURL = "https://vision.googleapis.com/v1/images:annotate?key=" + apiKey;

            string result = "";
            using (var client = new WebClient())
            {
                string jsonRequest = "";
                using (Image img = Image.FromFile(imgPath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        img.Save(m, img.RawFormat);
                        byte[] imgBytes = m.ToArray();

                        string imgBase64 = Convert.ToBase64String(imgBytes);

                        /// currently only utilizes the text detection feature
                        jsonRequest = "{ \"requests\":[ { \"image\":{ \"content\": \"" + imgBase64 +
                            "\"}, \"features\":[ { \"type\":\"TEXT_DETECTION\", \"maxResults\":10 } ] } ] }";
                    }
                }

                try
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    result = client.UploadString(visionURL, "POST", jsonRequest);
                }
                catch (Exception ex)
                {
                    // TODO: properly handle WebClient exceptions (how do we want to handle?)
                    Debug.WriteLine(ex);
                }
            }
            /// TODO: detect whether 1+ results are returned and handle cases properly
            return result;
        }
    }
}