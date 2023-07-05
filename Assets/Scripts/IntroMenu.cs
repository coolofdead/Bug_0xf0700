using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public string gameScene;
    public AnimationClip clickPlayBtnAnimationClip;

    public void LoadGameSceneAfterAnimation()
    {
        Invoke("LoadGameScene", clickPlayBtnAnimationClip.length);
    }

    private void LoadGameScene()
    {
        LoadScene(gameScene);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
