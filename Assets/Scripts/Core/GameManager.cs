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
        public int LubanTokens = 0;      // 椴佺彮浠?
        public int Money = 100;          // 鐩樼紶
        public int Fame = 0;             // 鍚嶆湜
        public float Satiety = 100f;     // 楗遍搴?
        public float Fatigue = 0f;       // 鍔崇疮鍊?

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
