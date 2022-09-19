using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.TOKEN_COLLECTED, OnTokenCollected);        
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TOKEN_COLLECTED, OnTokenCollected);
    }

    private void OnTokenCollected()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
