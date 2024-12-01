using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public UnityEngine.UI.Slider progressBar; // Optional: Use a slider for progress visualization.

    private void Start()
    {
        //LoadScene("Level1");
    }
    public async void LoadScene(string sceneName)
    {
        // Show loading screen
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        // Start the scene loading process
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Prevent the scene from activating immediately
        asyncOperation.allowSceneActivation = false;

        // Monitor loading progress
        while (!asyncOperation.isDone)
        {
            if (progressBar != null)
                progressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // Check if the scene is fully loaded
            if (asyncOperation.progress >= 0.9f)
            {
                // Optionally wait for a short duration before activating the scene
                await Task.Delay(500);

                // Allow the scene to activate
                asyncOperation.allowSceneActivation = true;
            }

            await Task.Yield(); // Let Unity process other tasks
        }

        // Hide the loading screen after the scene has fully loaded
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }


    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        LoadScene(currentSceneName);
    }
}
