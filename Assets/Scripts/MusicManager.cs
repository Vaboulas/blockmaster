using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("MusicManager instance is null!");
            }
            return instance;
        }
    }

    void Awake()
    {
        // Ensure only one instance exists and preserve it across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep music manager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate music managers
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on MusicManager object!");
        }

        audioSource.loop = true; // Loop music
        audioSource.Play(); // Start music
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute; // Toggle mute state
    }

    public bool IsMusicMuted()
    {
        return audioSource.mute; // Return mute state
    }
}
