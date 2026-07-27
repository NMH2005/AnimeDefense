using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private GameObject panel;
    private void Awake()
    {
        panel.SetActive(false);
    }

    public void Show()
    {
        panel.SetActive(true);
    }

    public void OnRestartClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}