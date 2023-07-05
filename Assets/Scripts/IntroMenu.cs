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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(FakeLoad());
    }

    private IEnumerator FakeLoad()
    {
        yield return new WaitForSeconds(clickPlayBtnAnimationClip.length);

        loadingText.SetActive(true);

        yield return new WaitForSeconds(extraFakeLoadingTime);

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
