using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; } // Singleton instance

    public string SharedData; // Example data to share between scenes

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // Enforce Singleton pattern
        }
    }

    public void UpdateData(string newData)
    {
        SharedData = newData;
    }
}
