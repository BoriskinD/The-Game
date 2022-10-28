using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    private int score = 0;

    public string firstScene = "Level 1";

    private void Awake()
    {
        Messenger.AddListener(GameEvent.TOKEN_COLLECTED, OnTokenCollected);
        Messenger.AddListener(GameEvent.PLAYER_DIED, OnGameOver);
        //GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TOKEN_COLLECTED, OnTokenCollected);
        Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnGameOver);
        //GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState currentGameState = GameStateManager.Instance.currentGamaeState;
            GameState newGameState = currentGameState == GameState.Runnig ? GameState.Paused : GameState.Runnig;
            GameStateManager.Instance.SetState(newGameState);
        }
    }

    private void OnTokenCollected()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void ExitToMainMenu()
    { 
        
    }

    //private void OnGameStateChanged(GameState newGameState)
    //{
    //    enabled = newGameState == GameState.Runnig;
    //}
}
