using System;
using System.IO;
using UnityEngine;

namespace TheBuildersJourney.Core
{
    [Serializable]
    public class PlayerProgress
    {
        public int lubanToken = 0;
        public int unlockedChapter = 1;
    }

    public static class SaveSystem
    {
        private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.json");

        public static void Save(PlayerProgress progress)
        {
            var json = JsonUtility.ToJson(progress, true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"[SaveSystem] Saved -> {SavePath}");
        }

        public static PlayerProgress LoadOrCreate()
        {
            if (!File.Exists(SavePath))
            {
                var init = new PlayerProgress();
                Save(init);
                return init;
            }

            var json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PlayerProgress>(json) ?? new PlayerProgress();
        }
    }
}
