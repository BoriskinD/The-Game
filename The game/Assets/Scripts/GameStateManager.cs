using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameStateManager() {}
    private static GameStateManager instance;

    public delegate void GameStateChangeHandler(GameState gameState);
    public event GameStateChangeHandler OnGameStateChanged;

    public GameState currentGamaeState { get; private set; }

    public static GameStateManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ();

            return instance;
        }
    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == currentGamaeState)
            return;

        currentGamaeState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}
