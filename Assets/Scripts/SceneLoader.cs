using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        SceneManager.LoadScene("Env", LoadSceneMode.Additive);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        Debug.Log("Scene " + scene.name + " is loaded");
    }
}
