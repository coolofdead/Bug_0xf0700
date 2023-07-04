using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UI_PopUp popup;

    private void Start()
    {
        StartCoroutine(Pop());
    }
    public void PlayGame()
    {
        // Loads the next scene in the build order
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Quits the game
        Application.Quit();
    }

    IEnumerator Pop()
    {
        yield return new WaitForSeconds(4f);
        popup.Pop();
    }
}
