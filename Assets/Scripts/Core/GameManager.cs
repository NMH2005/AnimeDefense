using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] private GameOverUI gameOverUI;

    private bool isGameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        if (isGameOver) return; 

        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.Show();
        }
    }

    public bool IsGameOver() => isGameOver;
}