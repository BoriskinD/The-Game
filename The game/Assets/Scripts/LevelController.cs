using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;

    public int tokenAmount = 35;

    private void Awake() =>  Messenger.AddListener(GameEvent.TOKEN_COLLECTED, OnTokenCount);

    private void OnDestroy() => Messenger.RemoveListener(GameEvent.TOKEN_COLLECTED, OnTokenCount);

    private void OnTokenCount()
    {
        tokenAmount--;
        if (tokenAmount == 0)
            EndGame();
    }

    private void EndGame()
    {
        Messenger.Broadcast(GameEvent.GAME_PAUSED);
        Managers.Audio.StopAllMusic();
        endGamePanel.SetActive(true);
    } 
}
