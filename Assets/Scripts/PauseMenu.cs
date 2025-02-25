using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign your Pause Menu panel in the Inspector
    public GameObject pauseButton;
    private bool isPaused = false;
    private bool isPauseButton = true;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu when the game starts
        Time.timeScale = 1; // Ensure the game is running normally
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Press ESC to toggle pause
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        isPauseButton = !isPauseButton;
        pauseMenuUI.SetActive(isPaused);
        pauseButton.SetActive(isPauseButton);
        Time.timeScale = isPaused ? 0 : 1; // Pause/unpause the game
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current level
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); // Load Main Menu scene
    }
}
