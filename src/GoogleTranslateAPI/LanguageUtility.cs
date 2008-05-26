/**
 * LanguageUtility.cs
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
using System.Linq;
using System.Text;

namespace Google.API.Translate
{
    public enum Language
    {
        Unknown,
        Arabic,
        Bulgarian,
        Catalan,
        Chinese,
        Chinese_Simplified,
        Chinese_Traditional,
        Croatian,
        Czech,
        Danish,
        Dutch,
        English,
        Estonian,
        Filipino,
        Finnish,
        French,
        German,
        Greek,
        Hebrew,
        Hindi,
        Hungarian,
        Indonesian,
        Italian,
        Japanese,
        Korean,
        Latvian,
        Lithuanian,
        Norwegian,
        Persian,
        Polish,
        Portuguese,
        Romanian,
        Russian,
        Serbian,
        Slovak,
        Slovenian,
        Spanish,
        Swedish,
        Thai,
        Turkish,
        Ukranian, 
        Vietnamese
    }

    public class LanguageUtility
    {
        private static readonly IDictionary<Language, string> s_LanguageCodeDict;

        private static readonly IList<Language> s_Translatable;

        static LanguageUtility()
        {
            s_LanguageCodeDict = new Dictionary<Language, string>();
            s_LanguageCodeDict[Language.Unknown] = "";
            s_LanguageCodeDict[Language.Arabic] = "ar";
            s_LanguageCodeDict[Language.Bulgarian] = "bg";
            s_LanguageCodeDict[Language.Catalan] = "ca";
            s_LanguageCodeDict[Language.Chinese] = "zh";
            s_LanguageCodeDict[Language.Chinese_Simplified] = "zh-CN";
            s_LanguageCodeDict[Language.Chinese_Traditional] = "zh-TW";
            s_LanguageCodeDict[Language.Croatian] = "hr";
            s_LanguageCodeDict[Language.Czech] = "cs";
            s_LanguageCodeDict[Language.Danish] = "da";
            s_LanguageCodeDict[Language.Dutch] = "nl";
            s_LanguageCodeDict[Language.English] = "en";
            s_LanguageCodeDict[Language.Estonian] = "et";
            s_LanguageCodeDict[Language.Filipino] = "tl";
            s_LanguageCodeDict[Language.Finnish] = "fi";
            s_LanguageCodeDict[Language.French] = "fr";
            s_LanguageCodeDict[Language.German] = "de";
            s_LanguageCodeDict[Language.Greek] = "el";
            s_LanguageCodeDict[Language.Hebrew] = "iw";
            s_LanguageCodeDict[Language.Hindi] = "hi";
            s_LanguageCodeDict[Language.Hungarian] = "hu";
            s_LanguageCodeDict[Language.Indonesian] = "id";
            s_LanguageCodeDict[Language.Italian] = "it";
            s_LanguageCodeDict[Language.Japanese] = "ja";
            s_LanguageCodeDict[Language.Korean] = "ko";
            s_LanguageCodeDict[Language.Latvian] = "lv";
            s_LanguageCodeDict[Language.Lithuanian] = "lt";
            s_LanguageCodeDict[Language.Norwegian] = "no";
            s_LanguageCodeDict[Language.Persian] = "fa";
            s_LanguageCodeDict[Language.Polish] = "pl";
            s_LanguageCodeDict[Language.Portuguese] = "pt-PT";
            s_LanguageCodeDict[Language.Romanian] = "ro";
            s_LanguageCodeDict[Language.Russian] = "ru";
            s_LanguageCodeDict[Language.Serbian] = "sr";
            s_LanguageCodeDict[Language.Slovak] = "sk";
            s_LanguageCodeDict[Language.Slovenian] = "sl";
            s_LanguageCodeDict[Language.Spanish] = "es";
            s_LanguageCodeDict[Language.Swedish] = "sv";
            s_LanguageCodeDict[Language.Thai] = "th";
            s_LanguageCodeDict[Language.Turkish] = "tr";
            s_LanguageCodeDict[Language.Ukranian] = "uk";
            s_LanguageCodeDict[Language.Vietnamese] = "vi";

            s_Translatable = new Language[]
                {
                    Language.Arabic,
                    Language.Bulgarian,
                    Language.Chinese_Simplified,
                    Language.Chinese_Traditional,
                    Language.Croatian,
                    Language.Czech,
                    Language.Danish,
                    Language.Dutch,
                    Language.English,
                    Language.Finnish,
                    Language.French,
                    Language.German,
                    Language.Greek,
                    Language.Hindi,
                    Language.Italian,
                    Language.Japanese,
                    Language.Korean,
                    Language.Norwegian,
                    Language.Polish,
                    Language.Portuguese,
                    Language.Romanian,
                    Language.Russian,
                    Language.Spanish,
                    Language.Swedish,
                };
        }

        public static IDictionary<Language, string> LanguageCodeDict
        {
            get
            {
                return s_LanguageCodeDict;
            }
        }

        public static IList<Language> Translatable
        {
            get
            {
                return s_Translatable;
            }
        }

        public static Language GetLanguage(string languageCode)
        {
            languageCode = languageCode.Trim();
            if(string.IsNullOrEmpty(languageCode))
            {
                return Language.Unknown;
            }
            foreach (KeyValuePair<Language, string> pair in LanguageCodeDict)
            {
                if(languageCode == pair.Value)
                {
                    return pair.Key;
                }
            }
            return Language.Unknown;
        }

        public static bool IsTranslatable(Language language)
        {
            return Translatable.Contains(language);
        }
    }
}
