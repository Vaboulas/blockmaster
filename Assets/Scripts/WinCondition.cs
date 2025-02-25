using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Needed to restart or load menu

public class WinCondition : MonoBehaviour
{
    public GameObject winPanel;
    public InputField nameInput;
    private HighScoreManager highScoreManager;
    private move2 moveScript;

    public GameObject LoseUI; // Assign your Pause Menu panel in the Inspector
    private bool lost = false;

    private Fall fallScript;

    public Transform player;

    void Start()
    {
        //highScoreManager = HighScoreManager.Instance;
        //moveScript = GetComponent<move2>();

        highScoreManager = Object.FindFirstObjectByType<HighScoreManager>(); // Ensure HighScoreManager exists
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not found! Make sure it's in the scene.");
        }

        moveScript = Object.FindFirstObjectByType<move2>(); // Find move script anywhere in the scene
        if (moveScript == null)
        {
            Debug.LogError("moveScript (move2) not found! Make sure it's attached to the player.");
        }

        fallScript = Object.FindFirstObjectByType<Fall>();
        if (fallScript == null)
        {
            Debug.LogError("Fall script not found! Falling detection might not work.");
        }

        LoseUI.SetActive(false); // Hide the pause menu when the game starts
        Time.timeScale = 1; // Ensure the game is running normally
    }

    private void Update()
    {
        if (fallScript != null && fallScript.HasFallen())
        {
            if (player.position.y > 10f)
            {
                LoseGame();
            }
            else if (player.position.y < 10f)
            {
                PlayerWins();
            }
        }
        else if (player.position.y < 5f)
        {
            LoseGame();
        }
    }

    public void PlayerWins()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0; // Pause game when winning
    }

    public void LoseGame()
    {
        lost = true;
        LoseUI.SetActive(lost);
        Time.timeScale = 0; // Pause/unpause the game
    }

    public void SubmitScore()
    {
        Debug.Log("Submitting score...");

        string playerName = nameInput.text;
        if (string.IsNullOrEmpty(playerName)) playerName = "Unknown";

        Debug.Log($"Submitting score: {playerName} - Moves: {moveScript.moveCount}");

        highScoreManager.AddHighScore(playerName, moveScript.moveCount);

        Time.timeScale = 1;
    }

}
