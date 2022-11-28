using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverPanelHandler : MonoBehaviour
{
    public TMP_Text gameOverText;

    public void ShowMenu(bool isActive, string text)
    {
        gameOverText.text = text;
        gameObject.SetActive(isActive);
    } 

    public void RestartGame() => SceneManager.LoadScene("Level 1");

    public void ExitToMainMenu() => SceneManager.LoadScene("Main Menu");
}
