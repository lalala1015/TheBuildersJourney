// 文件用途：游戏全局状态与核心玩家属性管理，提供场景切换与状态流转入口。
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheBuildersJourney.Core
{
    public enum GameState
    {
        Boot,
        MainMap,
        InLevel,
        Pause,
        Result
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [field: SerializeField] public GameState CurrentState { get; private set; } = GameState.Boot;

        // --- Core Player Stats ---
        [Header("Player Stats")]
        public int LubanTokens = 0;      // 鲁班币
        public int Money = 100;          // 盘缠
        public int Fame = 0;             // 名望
        public float Satiety = 100f;     // 饱食度
        public float Fatigue = 0f;       // 劳累值

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetState(GameState state)
        {
            CurrentState = state;
            Debug.Log($"[GameManager] State -> {state}");
        }

        public void LoadScene(string sceneName, GameState targetState)
        {
            SceneManager.LoadScene(sceneName);
            SetState(targetState);
        }
    }
}
