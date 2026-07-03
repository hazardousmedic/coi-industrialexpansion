using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Mafi.Localization;

namespace IndustrialExpansion
{
    internal static class ModTranslation
    {
        public static void Initialize(string modRootPath)
        {
            if (ModTranslation.s_initialized)
            {
                return;
            }
            ModTranslation.s_initialized = true;
            try
            {
                string cultureInfoId = LocalizationManager.CurrentLangInfo.CultureInfoId;
                string text = ((cultureInfoId.Length >= 2) ? cultureInfoId.Substring(0, 2) : "en");
                string text2 = Path.Combine(modRootPath, "translations");
                string text3 = Path.Combine(text2, text + ".json");
                if (!File.Exists(text3))
                {
                    text3 = Path.Combine(text2, "en.json");
                }
                if (File.Exists(text3))
                {
                    ModTranslation.s_translations = ModTranslation.ParseJson(File.ReadAllText(text3));
                }
            }
            catch (Exception)
            {
            }
        }

        public static string Get(string englishText)
        {
            string text;
            if (ModTranslation.s_translations.Count > 0 && ModTranslation.s_translations.TryGetValue(englishText, out text))
            {
                return text;
            }
            return englishText;
        }

        public static string GetFmt(string template, params object[] args)
        {
            string text = ModTranslation.Get(template);
            string text2;
            try
            {
                text2 = string.Format(text, args);
            }
            catch
            {
                text2 = string.Format(template, args);
            }
            return text2;
        }
        private static Dictionary<string, string> ParseJson(string json)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (object obj in Regex.Matches(json, "\"((?:[^\"\\\\]|\\\\.)*)\"\\s*:\\s*\"((?:[^\"\\\\]|\\\\.)*)\""))
            {
                Match match = (Match)obj;
                string text = ModTranslation.Unescape(match.Groups[1].Value);
                string text2 = ModTranslation.Unescape(match.Groups[2].Value);
                dictionary[text] = text2;
            }
            return dictionary;
        }

        private static string Unescape(string s)
        {
            return s.Replace("\\\"", "\"").Replace("\\\\", "\\").Replace("\\n", "\n")
                .Replace("\\t", "\t");
        }

        private static Dictionary<string, string> s_translations = new Dictionary<string, string>();

        private static bool s_initialized;
    }
}
