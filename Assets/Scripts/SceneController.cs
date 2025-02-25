using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneNameToLoad; // Name of the scene to load
    public string dataToPass;     // Data to pass when switching scenes

    public void LoadScene()
    {
        // Update the data in the Singleton
        DataManager.Instance.UpdateData(dataToPass);

        // Load the target scene
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
