/**
 * TestTranslator.cs
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
using System.Collections.Generic;
using NUnit.Framework;

namespace Google.API.Translate.Test
{
    [TestFixture]
    public class TestTranslator
    {
        private static readonly ICollection<Language> s_Undetectable = new Language[]
            {
                Language.Chinese_Simplified,
                Language.Croatian,
                Language.Greek,
                Language.Hindi,
                Language.Norwegian,
            };

        [Test]
        public void TranslateTest()
        {
            Language originalLanguage = Language.English;
            string originalText = "cat";

            Print(originalLanguage, originalText);

            foreach (Language language in LanguageUtility.translatableCollection)
            {
                if (language == originalLanguage)
                {
                    continue;
                }
                string translatedText = Translator.Translate(originalText, originalLanguage, language);
                Assert.AreNotEqual(originalText, translatedText,
                                   "[{0} -> {1}] {2} : translate failed! Because the result is same to the original one.",
                                   originalLanguage, language, originalText);

                Print(language, translatedText);

                string transbackText = Translator.Translate(translatedText, language, originalLanguage);
                StringAssert.AreEqualIgnoringCase(originalText, transbackText, "[{0} -> {1}] {2} -> {3} != {4}: translate faild!",
                                language, originalLanguage, translatedText, transbackText, originalText);
            }
        }

        [Test]
        public void TranslateTestForHtml()
        {
            // TODO : The test case TranslateTestForHtml is not stable. There may add some space after being translated.
            
            Language from = Language.English;
            Language to = Language.Chinese_Simplified;

            string textTemplate =
                "<html><head><title>{0} </title></head><body> <b>{1}</b> </body></html>";

            string sentenceA = "You are my sunshine.";
            string sentenceB = "Show me the money.";

            string text = string.Format(textTemplate, sentenceA, sentenceB);

            string translatedA = Translator.Translate(sentenceA, from, to);

            string translatedB = Translator.Translate(sentenceB, from, to);

            string translatedText = Translator.Translate(text, from, to, TranslateFormat.html);

            string expectedText = string.Format(textTemplate, translatedA, translatedB);

            StringAssert.AreEqualIgnoringCase(expectedText, translatedText,
                                              string.Format("expected:\t{1}{0}actual:\t{2}", Environment.NewLine,
                                                            expectedText, translatedText));
        }

        [Test]
        public void DetectTest()
        {
            Language originalLanguage = Language.English;
            string originalText = "This is an apple. I love apple. I eat apple everyday.";

            Print(originalLanguage, originalText);

            foreach (Language language in LanguageUtility.translatableCollection)
            {
                if (language == originalLanguage)
                {
                    continue;
                }

                if (IsUndetectable(language))
                {
                    continue;
                }

                string translatedText = Translator.Translate(originalText, originalLanguage, language);
                Assert.AreNotEqual(originalText, translatedText,
                                   "[{0} -> {1}] {2} : translate failed! Because the result is same to the original one.",
                                   originalLanguage, language, originalText);

                bool isReliable;
                double confidence;
                Language detectedLanguage = Translator.Detect(translatedText, out isReliable, out confidence);

                string more = string.Format("isReliable : {0}, confidence : {1}", isReliable, confidence);
                Print(language, translatedText, more);

                Assert.AreEqual(language, detectedLanguage,
                                "[{0} != {1}] {2} ({3}): detect failed!"
                                , detectedLanguage, language, translatedText, more);
            }
        }

        private static bool IsUndetectable(Language language)
        {
            return s_Undetectable.Contains(language);
        }

        private static void Print(Language language, string text, string more)
        {
            Console.WriteLine("[{0}]\t{1}\t{2}", language, text, more);
        }

        private static void Print(Language language, string text)
        {
            Console.WriteLine("[{0}]\t{1}", language, text);
        }
    }
}
