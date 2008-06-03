using System;
using System.IO;
using System.Net;
using System.Text;
using Google.API.Translate;
using Newtonsoft.Json;

namespace Google.API
{
    internal class RequestUtility
    {
        private static readonly Encoding s_Encoding = Encoding.UTF8;

        public static T GetResponseData<T>(WebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            string resultString;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), s_Encoding))
                    {
                        resultString = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                throw new GoogleAPIException("Failed to get response.", ex);
            }
            catch (IOException ex)
            {
                throw new GoogleAPIException("Cannot read the response stream.", ex);
            }

            ResultObject<T> resultObject;
            try
            {
                resultObject = JavaScriptConvert.DeserializeObject<ResultObject<T>>(resultString);
            }
            catch(JsonSerializationException ex)
            {
                throw new GoogleAPIException(
                    string.Format("Deserialize Failed.{0}Type:{1}{0}String:{2}", Environment.NewLine,
                                  typeof (ResultObject<T>), resultString));
            }

            if (resultObject.ResponseStatus != 200)
            {
                throw new GoogleAPIException(string.Format("[error code:{0}]{1}", resultObject.ResponseStatus, resultObject.ResponseDetails));
            }
            return resultObject.ResponseData;
        }

        public static T GetResponseData<T>(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            WebRequest request = WebRequest.Create(url);
            return GetResponseData<T>(request);
        }
    }
}
