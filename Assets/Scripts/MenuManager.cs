using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Important for scene loading
using TMPro; // Important for TextMeshPro

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenu;
    private bool isMain = true;
    public GameObject levelsUI; 
    private bool isLevels = false;
    public GameObject highscoreUI; 
    private bool isScores = false;

    private int highScore;

    public Button muteButton; // Your mute button
    private MusicManager musicManager;

    void Start()
    {
        musicManager = MusicManager.Instance; // Access the singleton

        if (musicManager != null)
        {
            UpdateMuteButtonText();
            muteButton.onClick.AddListener(musicManager.ToggleMusic); // Connect the button to the toggle function
        }
        else
        {
            Debug.LogError("MusicManager not found!");
        }
    }
    void UpdateMuteButtonText()
    {
        if (musicManager != null)
        {
            Text buttonText = muteButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = musicManager.IsMusicMuted() ? "Unmute" : "Mute";
            }
        }
    }

    void OnEnable()
    {
        UpdateMuteButtonText();
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the game (works in builds, not in the editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For quitting in the editor
#endif
    }

    // ... functions for other levels
    public void ToggleLevels()
    {
        isLevels = !isLevels;
        isMain = !isMain;
        mainMenu.SetActive(isMain);
        levelsUI.SetActive(isLevels);
    }

    public void ToggleScores()
    {
        isScores = !isScores;
        isMain = !isMain;
        mainMenu.SetActive(isMain);
        highscoreUI.SetActive(isScores);
    }

}