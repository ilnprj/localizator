﻿namespace LocalizatorSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using SimpleJSON;
    using System;
    
    /// <summary>
    /// JSON parser that takes the necessary parameters from the localization files
    /// </summary>
    public class LocalizeJSON : IParseableLocalize
    {
        public const string PATH = "LocJSON";

        public List<string> AvailableLanguages {get;set;}
        public Dictionary<string, string> ParsedLocalization { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> GetParsedLocalization()
        {
            return ParsedLocalization;
        }

        public void InitParseModule(string currentLanguage)
        {
            ParsedLocalization = new Dictionary<string, string>();
            AvailableLanguages = new List<string>();
            TextAsset data;
            Debug.LogError("INIT");
            try
            {
                data = Resources.Load<TextAsset>(PATH);
            }
            catch (Exception e)
            {
                Debug.LogError("File " + PATH + ".json is not found in folder Resources!");
                Debug.LogError(e.Message);
                return;
            }

            var jSON = JSON.Parse(data.text);
            var langs  = jSON["Languages"];
            var localizations = jSON["Localizations"];

            foreach (var item in langs)
            {
                AvailableLanguages.Add(item.Value.ToString());
            }

            string key = " ";
            string value = " ";
            
            for (int i = 0; i < localizations.Count; i++)
            {
                key = localizations[i][0]["key"].ToString();
                Debug.LogError("KEY = "+key);
                if (!string.Equals(localizations[i][0][currentLanguage].ToString(), "null"))
                {
                    value = localizations[i][0][currentLanguage].ToString();
                }
                else
                {
                    value = localizations[i][0][1].ToString();
                }
                key = key.Replace("\"", string.Empty);
                value = value.Replace("\"", string.Empty);
                ParsedLocalization.Add(key, value);
            }
        }
    }
}
