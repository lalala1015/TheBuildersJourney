using System;
using System.IO;
using UnityEngine;

namespace TheBuildersJourney.Core
{
    [Serializable]
    public class PlayerProgress
    {
        public int lubanToken = 0;      // 椴佺彮浠?
        public int unlockedChapter = 1; // 宸茶В閿佺珷鑺?

        // Survival & Economy Stats
        public int money = 100;         // 鐩樼紶
        public int fame = 0;            // 鍚嶆湜
        public float satiety = 100f;    // 楗遍搴?
        public float fatigue = 0f;      // 鍔崇疮鍊?
        
        // Collection
        public int discoveredWikiCards = 0; // 鍥鹃壌鏀堕泦杩涘害
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
