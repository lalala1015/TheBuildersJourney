using System;
using UnityEngine;

namespace TheBuildersJourney.UI
{
    [Serializable]
    public class DialogLine
    {
        public string speaker;
        public string content;
    }

    [Serializable]
    public class DialogData
    {
        public string id;
        public DialogLine[] lines;
    }

    public class DialogSystem : MonoBehaviour
    {
        [SerializeField] private string dialogConfigName = "dialogue_mainmap";

        private DialogData dialogData;
        private int currentIndex = 0;

        private void Start()
        {
            Load(dialogConfigName);
            ShowCurrent();
        }

        public void Load(string configName)
        {
            var textAsset = Resources.Load<TextAsset>($"Configs/{configName}");
            if (textAsset == null)
            {
                Debug.LogError($"[DialogSystem] Config not found: {configName}");
                return;
            }

            dialogData = JsonUtility.FromJson<DialogData>(textAsset.text);
            currentIndex = 0;
        }

        public void Next()
        {
            if (dialogData?.lines == null || dialogData.lines.Length == 0) return;

            currentIndex++;
            if (currentIndex >= dialogData.lines.Length)
            {
                Debug.Log("[DialogSystem] Dialog finished.");
                return;
            }

            ShowCurrent();
        }

        private void ShowCurrent()
        {
            if (dialogData?.lines == null || dialogData.lines.Length == 0) return;
            var line = dialogData.lines[currentIndex];
            Debug.Log($"[{line.speaker}] {line.content}");
        }
    }
}
