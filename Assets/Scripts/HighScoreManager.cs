using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int moves;
}

[System.Serializable]
public class HighScoreData
{
    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
}

public class HighScoreManager : MonoBehaviour
{
    private string filePath;
    private string currentLevel;
    public HighScoreData highScoreData = new HighScoreData();

    public static HighScoreManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateFilePath();
        LoadHighScores();
    }

    void UpdateFilePath()
    {
        currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        filePath = Application.persistentDataPath + $"/highscores_{currentLevel}.json";
       // Debug.Log($"Updated high score file path: {filePath}");
    }

    public void AddHighScore(string playerName, int moves)
    {
        HighScoreEntry newEntry = new HighScoreEntry { playerName = playerName, moves = moves };

        // Add to the list and sort (lowest moves first)
        highScoreData.highScores.Add(newEntry);
        highScoreData.highScores.Sort((a, b) => a.moves.CompareTo(b.moves));

        // Keep only the top 10 scores
        if (highScoreData.highScores.Count > 10)
        {
            highScoreData.highScores.RemoveAt(10);
        }

        SaveHighScores();
    }

    public void SaveHighScores()
    {
        string json = JsonUtility.ToJson(highScoreData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("HighScore file path: " + filePath);

    }

    public void LoadHighScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        }
    }

    public List<HighScoreEntry> GetHighScores()
    {
        return highScoreData.highScores;
    }

    public void LoadHighScoresForLevel(string levelName)
    {
        string levelFilePath = Application.persistentDataPath + $"/highscores_{levelName}.json";

        Debug.Log("Loading HighScores from: " + levelFilePath);

        if (File.Exists(levelFilePath))
        {
            string json = File.ReadAllText(levelFilePath);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);

            Debug.Log("Loaded HighScores: " + json);
        }
        else
        {
            highScoreData = new HighScoreData();// Create an empty list if no file exists
            Debug.LogWarning("No HighScore file found for " + levelName);
        }
    }

}
