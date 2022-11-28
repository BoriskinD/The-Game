using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public string firstScene = "Level 1";

    [SerializeField] private GameObject gameOverPanel;
    private GameOverPanelHandler gameOverPanelHandler;

    private string gameOverText = "Вы проиграли!";
    private string gamePauseText = "Пауза!";

    private bool gameOnPause = false;

    private void Awake()
    {
        gameOverPanelHandler = gameOverPanel.GetComponent<GameOverPanelHandler>();
        Messenger.AddListener(GameEvent.PLAYER_DIED, OnPlayerDided);
    }

    private void OnDestroy() => Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnPlayerDided);

    private void Start() => Managers.Audio.PlayBGMusic();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOnPause)
            {
                gameOnPause = false;
                Messenger.Broadcast(GameEvent.GAME_UNPAUSED);
                Managers.Audio.PlayBGMusic();
                gameOverPanelHandler.ShowMenu(false, gameOverText);
            }
            else
            {
                gameOnPause = true;
                Messenger.Broadcast(GameEvent.GAME_PAUSED);
                Managers.Audio.PlayPMenuMusic();
                gameOverPanelHandler.ShowMenu(true, gamePauseText);
            } 
        }
    }

    private void OnPlayerDided() => gameOverPanelHandler.ShowMenu(true, gameOverText);
}
