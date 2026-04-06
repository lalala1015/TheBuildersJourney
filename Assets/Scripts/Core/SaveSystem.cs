// 文件用途：定义玩家进度数据结构，并负责本地 JSON 存档的保存与加载。
using System;
using System.IO;
using UnityEngine;

namespace TheBuildersJourney.Core
{
    [Serializable]
    public class PlayerProgress
    {
        public int lubanToken = 0;      // 鲁班币
        public int unlockedChapter = 1; // 已解锁章节

        // Survival & Economy Stats
        public int money = 100;         // 盘缠
        public int fame = 0;            // 名望
        public float satiety = 100f;    // 饱食度
        public float fatigue = 0f;      // 劳累值
        
        // Collection
        public int discoveredWikiCards = 0; // 图鉴收集进度
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
