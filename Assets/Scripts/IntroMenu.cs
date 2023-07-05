using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public string gameScene;
    public AnimationClip clickPlayBtnAnimationClip;
    public GameObject loadingText;
    public float extraFakeLoadingTime = 2f;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.UnloadScene("IntroScene");
    }

    public void LoadGameSceneAfterAnimation()
    {
        Invoke("LoadGameScene", clickPlayBtnAnimationClip.length + extraFakeLoadingTime);
    }

    private void LoadGameScene()
    {
        loadingText.SetActive(true);
        LoadScene(gameScene);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
