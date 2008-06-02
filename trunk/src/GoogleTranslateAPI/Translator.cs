/**
 * Translator.cs
 *
 * Copyright (C) 2008,  iron9light
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Google.API.Translate
{
    public enum TranslateFormat
    {
        text,
        html,
    }

    /// <summary>
    /// Utility class for translate and detect.
    /// </summary>
    public class Translator
    {
        private static readonly Encoding ENCODING = Encoding.UTF8;

        /// <summary>
        /// Translate the text from <paramref name="from"/> to <paramref name="to"/>
        /// </summary>
        /// <param name="text">the content will be translated</param>
        /// <param name="from">the language of the original text. You can set it as <c>Language.Unknown</c> to the auto detect it.</param>
        /// <param name="to">the target language you want to translate to.</param>
        /// <returns>the translate result</returns>
        /// <exception cref="TranslateException">bad luck.</exception>
        public static string Translate(string text, Language from, Language to)
        {
            return Translate(text, from, to, TranslateFormat.text);
        }

        /// <summary>
        /// Translate the text from <paramref name="from"/> to <paramref name="to"/>
        /// </summary>
        /// <param name="text">the content will be translated</param>
        /// <param name="from">the language of the original text. You can set it as <c>Language.Unknown</c> to the auto detect it.</param>
        /// <param name="to">the target language you want to translate to.</param>
        /// <param name="format">The format of the text.</param>
        /// <returns>the translate result</returns>
        /// <exception cref="TranslateException">bad luck.</exception>
        public static string Translate(string text, Language from, Language to, TranslateFormat format)
        {
            if (from != Language.Unknown && !LanguageUtility.IsTranslatable(from))
            {
                throw new TranslateException("Can not translate this language : " + from);
            }
            if (!LanguageUtility.IsTranslatable(to))
            {
                throw new TranslateException(string.Format("Can not translate this language to \"{0}\"", to));
            }
            TranslateData result;
            try
            {
                result = Translate(text, LanguageUtility.GetLanguageCode(from), LanguageUtility.GetLanguageCode(to), format);
            }
            catch (TranslateException ex)
            {
                throw new TranslateException("Translate failed!", ex);
            }
            return result.TranslatedText;
        }

        /// <summary>
        /// Detect the language for this text.
        /// </summary>
        /// <param name="text">the text you want to test.</param>
        /// <param name="isReliable">whether the result is reliable</param>
        /// <param name="confidence">the confidence percent of the result</param>
        /// <returns>the detected language</returns>
        public static Language Detect(string text, out bool isReliable, out double confidence)
        {
            DetectData result;
            try
            {
                result = Detect(text);
            }
            catch (TranslateException ex)
            {
                throw new TranslateException("Detect failed!", ex);
            }
            string languageCode = result.LanguageCode;
            isReliable = result.IsReliable;
            confidence = result.Confidence;
            Language language = LanguageUtility.GetLanguage(languageCode);
            return language;
        }

        internal static TranslateData Translate(string text, string from, string to)
        {
            return Translate(text, from, to, TranslateFormat.text);
        }

        internal static TranslateData Translate(string text, string from, string to, TranslateFormat format)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            //string urlString = BuildTranslateUrl(text, from, to);
            //TranslateData responseData = GetResponseData<TranslateData>(urlString);

            TranslateRequest request = new TranslateRequest(text, from, to, format);
            TranslateData responseData;
            try
            {
                responseData = GetResponseData<TranslateData>(request.GetWebRequest());
            }
            catch (TranslateException ex)
            {
                throw new TranslateException(string.Format("request:\"{0}\"", request), ex);
            }

            return responseData;
        }

        internal static DetectData Detect(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            DetectRequest request = new DetectRequest(text);
            DetectData responseData;
            try
            {
                responseData = GetResponseData<DetectData>(request.GetWebRequest());
            }
            catch(TranslateException ex)
            {
                throw new TranslateException(string.Format("request:\"{0}\"", request), ex);
            }

            return responseData;
        }

        internal static T GetResponseData<T>(WebRequest request)
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
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), ENCODING))
                    {
                        resultString = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                throw new TranslateException("Failed to get response.", ex);
            }
            catch (IOException ex)
            {
                throw new TranslateException("Cannot read the response stream.", ex);
            }
            ResultObject<T> resultObject = JavaScriptConvert.DeserializeObject<ResultObject<T>>(resultString);
            if (resultObject.ResponseStatus != 200)
            {
                throw new TranslateException(string.Format("[error code:{0}]{1}", resultObject.ResponseStatus, resultObject.ResponseDetails));
            }
            return resultObject.ResponseData;
        }

        internal static T GetResponseData<T>(string url)
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
