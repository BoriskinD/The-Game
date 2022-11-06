using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public string firstScene = "Level 1";

    [SerializeField] private GameObject gameOverPanel;
    private bool gameOnPause = false;

    private void Awake() => Messenger.AddListener(GameEvent.PLAYER_DIED, OnPlayerDided);

    private void OnDestroy() => Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnPlayerDided);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOnPause)
            {
                gameOnPause = false;
                Messenger.Broadcast(GameEvent.GAME_UNPAUSED);
                Managers.Audio.PlayBGMusic();
            }
            else
            {
                gameOnPause = true;
                Messenger.Broadcast(GameEvent.GAME_PAUSED);
                Managers.Audio.PlayPauseMenuMusic();
            } 
        }
    }

    private void OnPlayerDided()
    {
        gameOverPanel.GetComponent<GameOverPanelHandler>().ShowMenu();
    }
}
