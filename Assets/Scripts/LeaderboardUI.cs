using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] public Dropdown levelDropdown; // Dropdown to select levels
    [SerializeField] public GameObject scoreEntryTemplatePrefab; // UI template for displaying scores
    [SerializeField] public Transform scoreListContainer; // Container for score entries
    [SerializeField] public Button loadButton; // Load button

    private HighScoreManager highScoreManager;

    void Start()
    {
        highScoreManager = Object.FindFirstObjectByType<HighScoreManager>();
        LoadLevelDropdown();

        // Add a listener to the Load button (instead of the dropdown)
        loadButton.onClick.AddListener(LoadHighScores);
    }

    void LoadLevelDropdown()
    {
        levelDropdown.ClearOptions();

        // Add levels manually or dynamically if they are known
        List<string> levels = new List<string> { "Level1", "Level2", "Level3" };
        levelDropdown.AddOptions(levels);

        // Removed the event listener for dropdown changes!
    }

    public void LoadHighScores()
    {
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager is NULL! Make sure it's assigned.");
            return;
        }

        string selectedLevel = levelDropdown.options[levelDropdown.value].text;
        Debug.Log($"Loading high scores for: {selectedLevel}");

        highScoreManager.LoadHighScoresForLevel(selectedLevel);
        List<HighScoreEntry> scores = highScoreManager.GetHighScores();

        // Clear previous entries
        foreach (Transform child in scoreListContainer)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }

        Debug.Log($"Found {scores.Count} high scores.");

        // Display new scores
        foreach (HighScoreEntry entry in scores)
        {
            if (scoreEntryTemplatePrefab == null)
            {
                Debug.LogError("Score entry template is NULL! Make sure it's assigned.");
                return;
            }

            GameObject newEntry = Instantiate(scoreEntryTemplatePrefab, scoreListContainer);
            if (newEntry == null)
            {
                Debug.LogError("Failed to instantiate score entry!");
                continue;
            }

            newEntry.SetActive(true);

            Text entryText = newEntry.GetComponent<Text>();
            if (entryText == null)
            {
                Debug.LogError("No Text component found on scoreEntryTemplate!");
                continue;
            }

            entryText.text = $"{entry.playerName} - {entry.moves} Moves";
            entryText.enabled = true;
        }
    }
}
