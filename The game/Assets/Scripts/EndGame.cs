using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void ExitToMainMenu() => SceneManager.LoadScene(0);
}
