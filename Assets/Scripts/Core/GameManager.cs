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
