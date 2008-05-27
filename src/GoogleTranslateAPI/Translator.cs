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
    public class Translator
    {
        private static readonly Encoding ENCODING = Encoding.UTF8;
        private static readonly string translateUrl = "http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}%7C{2}";
        private static readonly string detectUrl = "http://ajax.googleapis.com/ajax/services/language/detect?v=1.0&q={0}";

        public static string Translate(string text, Language from, Language to)
        {
            if (from != Language.Unknown && !LanguageUtility.IsTranslatable(from))
            {
                from = Language.Unknown;
            }
            if (!LanguageUtility.IsTranslatable(to))
            {
                return string.Empty;
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

        public static TranslateResult Translate(string text, string from, string to)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            string urlString = BuildTranslateUrl(text, from, to);

            TranslateResult resultObject = GetResultObject<TranslateResult>(urlString);

            return resultObject;
        }

        static string BuildTranslateUrl(string text, string from, string to)
        {
            string newText = HttpUtility.UrlEncode(text);
            string result = string.Format(translateUrl, newText, from, to);
            return result;
        }

        public static Language Detect(string text, out bool isReliable, out double confidence)
        {
            DetectResult result;
            try
            {
                result = Detect(text);
            }
            catch(TranslateException ex)
            {
                throw new TranslateException("Detect failed!", ex);
            }
            if(result.ResponseData == null)
            {
                throw new TranslateException(result.ResponseDetails);
            }
            string languageCode = result.ResponseData.LanguageCode;
            isReliable = result.ResponseData.IsReliable;
            confidence = result.ResponseData.Confidence;
            Language language = LanguageUtility.GetLanguage(languageCode);
            return language;
        }

        public static DetectResult Detect(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            string urlString = BuildDetectUrl(text);

            DetectResult resultObject = GetResultObject<DetectResult>(urlString);

            return resultObject;
        }

        static string BuildDetectUrl(string text)
        {
            string newText = HttpUtility.UrlEncode(text);
            string result = string.Format(detectUrl, newText);
            return result;
        }

        static TResult GetResultObject<TResult>(string url)
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
