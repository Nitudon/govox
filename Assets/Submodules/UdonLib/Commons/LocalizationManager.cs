using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UdonLib.Commons
{
    public class LocalizationManager : UdonBehaviourSingleton<LocalizationManager>
    {
        private const string LOCALIZATION_FILE_PATH = "Localization.txt";
        private const char KEY_VALUE_SPLIT = ',';

        private Dictionary<string, string> _localizedTextMap;

        public override void Initialize()
        {
            base.Initialize();
            _localizedTextMap = new Dictionary<string, string>();
            LoadLocalizationFile();
        }

        public string GetValue(string key)
        {
            string value;
            if (_localizedTextMap.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                InstantLog.StringLogWarning("Invalid Localization Key");
                return key;
            }
        }

        private void LoadLocalizationFile()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, LOCALIZATION_FILE_PATH);

            if(File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                for(int i = 0; i < lines.Length; ++i)
                {
                    var split = lines[i].Split(KEY_VALUE_SPLIT);
                    if(split == null || split.Length != 2)
                    {
                        continue;
                    }
                    _localizedTextMap.Add(split[0], split[1]);
                }
            }
            else
            {
                InstantLog.StringLogError("Missing Localization File");
            }
        }
    }

    [System.Serializable]
    public class LocalizationData
    {
        public LocalizationItem[] items;
    }

    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
}
