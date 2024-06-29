using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    [SerializeField] private GameObject splashScreen;
    
    public static SceneLoader Instance { get; private set; }

    private string _currentScene = "";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void LoadScene(string sceneName)
    {
        // Show the splash screen
        splashScreen.SetActive(true);
        
        //check if we have a scene loaded already, and then unload it
        if (_currentScene != "")
        {
            SceneManager.UnloadSceneAsync(_currentScene);
        }
        
        // load the new scene
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _currentScene = sceneName;
        
        // Listen to the scene load
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }

    // This method will execute everytime a new scene is completed its loading process.
    private void OnNewSceneLoaded(Scene sceneName, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(sceneName);
        
        // Hide the splash screen
        splashScreen.SetActive(false);
        
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }
}
