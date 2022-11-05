using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelHandler : MonoBehaviour
{
    public void ShowMenu() => gameObject.SetActive(true);

    public void RestartGame() => SceneManager.LoadScene("Level 1");

    public void ExitToMainMenu() => SceneManager.LoadScene("Main Menu");
}
