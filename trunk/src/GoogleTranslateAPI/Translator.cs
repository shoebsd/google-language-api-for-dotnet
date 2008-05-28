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
using System.Web;
using Newtonsoft.Json;

namespace Google.API.Translate
{
    /// <summary>
    /// Utility class for translate and detect.
    /// </summary>
    public class Translator
    {
        private static readonly Encoding ENCODING = Encoding.UTF8;
        private static readonly string translateUrl = "http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}%7C{2}";
        private static readonly string detectUrl = "http://ajax.googleapis.com/ajax/services/language/detect?v=1.0&q={0}";

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
            if (from != Language.Unknown && !LanguageUtility.IsTranslatable(from))
            {
                throw new TranslateException("Can not translate this language : " + from);
            }
            if (!LanguageUtility.IsTranslatable(to))
            {
                throw new TranslateException(string.Format("Can not translate this language to \"{0}\"", to));
            }
            TranslateResult result;
            try
            {
                result = Translate(text, LanguageUtility.GetLanguageCode(from), LanguageUtility.GetLanguageCode(to));
            }
            catch (TranslateException ex)
            {
                throw new TranslateException("Translate failed!", ex);
            }
            if(result.ResponseData == null)
            {
                throw new TranslateException(result.ResponseDetails);
            }
            return result.ResponseData.TranslatedText;
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
            DetectResult result;
            try
            {
                result = Detect(text);
            }
            catch (TranslateException ex)
            {
                throw new TranslateException("Detect failed!", ex);
            }
            if (result.ResponseData == null)
            {
                throw new TranslateException(result.ResponseDetails);
            }
            string languageCode = result.ResponseData.LanguageCode;
            isReliable = result.ResponseData.IsReliable;
            confidence = result.ResponseData.Confidence;
            Language language = LanguageUtility.GetLanguage(languageCode);
            return language;
        }

        internal static TranslateResult Translate(string text, string from, string to)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            string urlString = BuildTranslateUrl(text, from, to);

            TranslateResult resultObject = GetResultObject<TranslateResult>(urlString);

            return resultObject;
        }

        internal static DetectResult Detect(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            string urlString = BuildDetectUrl(text);

            DetectResult resultObject = GetResultObject<DetectResult>(urlString);

            return resultObject;
        }

        private static string BuildTranslateUrl(string text, string from, string to)
        {
            string newText = HttpUtility.UrlEncode(text);
            string result = string.Format(translateUrl, newText, from, to);
            return result;
        }

        private static string BuildDetectUrl(string text)
        {
            string newText = HttpUtility.UrlEncode(text);
            string result = string.Format(detectUrl, newText);
            return result;
        }

        private static TResult GetResultObject<TResult>(string url)
        {
            WebRequest request = WebRequest.Create(url);
            TResult resultObject;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), ENCODING))
                {
                    string resultString = reader.ReadToEnd();
                    resultObject = JavaScriptConvert.DeserializeObject<TResult>(resultString);
                }
            }
            return resultObject;
        }
    }
}
