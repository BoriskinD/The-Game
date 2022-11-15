using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverPanelHandler : MonoBehaviour
{
    public TMP_Text gameOverText;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.AddListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.RemoveListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
    }

    private void OnGamePaused() => gameOverText.text = "Пауза";

    private void OnGameUnPaused() => gameOverText.text = "Вы проиграли!";

    public void ShowMenu(bool isActive) => gameObject.SetActive(isActive);

    public void RestartGame() => SceneManager.LoadScene("Level 1");

    public void ExitToMainMenu() => SceneManager.LoadScene("Main Menu");
}
