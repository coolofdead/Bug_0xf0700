using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public Image continueButton;
    public Color continueButtonIdleColor;
    public float continueButtonIdleTime = 0.7f;
    public AnimationClip showPayCheck;
    public GameObject loadingText;

    public float fakeDelayToLoadIntroScene = 2f;

    [Header("Score")]
    public float playerObjectScore;
    public float lightScore;
    public float timeScore;
    public float computerHackedScore;
    public float computerFiredScore;
    public float computerFixedScore;

    [Header("Total Total")]
    public TextMeshProUGUI totalTMP;

    [Header("Nb")]
    public TextMeshProUGUI nbPlayerObjectsTMP;
    public TextMeshProUGUI nbLightsTMP;
    public TextMeshProUGUI nbTimeTMP;
    public TextMeshProUGUI nbComputerHackedTMP;
    public TextMeshProUGUI nbComputerFiredTMP;
    public TextMeshProUGUI nbComputerFixedTMP;

    [Header("Score")]
    public TextMeshProUGUI scorePlayerObjectsTMP;
    public TextMeshProUGUI scoreLightsTMP;
    public TextMeshProUGUI scoreTimeTMP;
    public TextMeshProUGUI scoreComputerHackedTMP;
    public TextMeshProUGUI scoreComputerFiredTMP;
    public TextMeshProUGUI scoreComputerFixedTMP;

    [Header("Total")]
    public TextMeshProUGUI totalPlayerObjectsTMP;
    public TextMeshProUGUI totalLightsTMP;
    public TextMeshProUGUI totalTimeTMP;
    public TextMeshProUGUI totalComputerHackedTMP;
    public TextMeshProUGUI totalComputerFiredTMP;
    public TextMeshProUGUI totalComputerFixedTMP;

    private void Awake()
    {
        TimeManager.onTimeOver += OnTimeOver;
    }

    private void OnTimeOver()
    {
        endGamePanel.SetActive(true);

        nbPlayerObjectsTMP.text = PlayerInteractionController.NbTimeObjectPicked.ToString();
        nbPlayerObjectsTMP.text = LightEvent.NbLightEvents.ToString();
        nbPlayerObjectsTMP.text = TimeManager.Instance.levelDurationInMinutes.ToString();
        nbPlayerObjectsTMP.text = Computer.TotalComputerPutOnFire.ToString();
        nbPlayerObjectsTMP.text = BugsManager.Instance.TotalOfComputersFixed.ToString();
        nbPlayerObjectsTMP.text = BugsManager.Instance.TotalOfComputersHacked.ToString();

        scorePlayerObjectsTMP.text = playerObjectScore.ToString();
        scorePlayerObjectsTMP.text = lightScore.ToString();
        scorePlayerObjectsTMP.text = timeScore.ToString();
        scorePlayerObjectsTMP.text = computerHackedScore.ToString();
        scorePlayerObjectsTMP.text = computerFiredScore.ToString();
        scorePlayerObjectsTMP.text = computerFixedScore.ToString();

        totalPlayerObjectsTMP.text = (PlayerInteractionController.NbTimeObjectPicked * playerObjectScore).ToString();
        totalPlayerObjectsTMP.text = (LightEvent.NbLightEvents * lightScore).ToString();
        totalPlayerObjectsTMP.text = (TimeManager.Instance.levelDurationInMinutes * timeScore).ToString();
        totalPlayerObjectsTMP.text = (Computer.TotalComputerPutOnFire * computerFiredScore).ToString();
        totalPlayerObjectsTMP.text = (BugsManager.Instance.TotalOfComputersFixed * computerFixedScore).ToString();
        totalPlayerObjectsTMP.text = (BugsManager.Instance.TotalOfComputersHacked * computerHackedScore).ToString();

        totalTMP.text = (
            PlayerInteractionController.NbTimeObjectPicked * playerObjectScore +
            LightEvent.NbLightEvents * lightScore +
            TimeManager.Instance.levelDurationInMinutes * timeScore +
            Computer.TotalComputerPutOnFire * computerFiredScore +
            BugsManager.Instance.TotalOfComputersFixed * computerFixedScore +
            BugsManager.Instance.TotalOfComputersHacked * computerHackedScore
        ).ToString();

        StartCoroutine(ShowEnd());
    }

    private IEnumerator ShowEnd()
    {
        yield return new WaitForSeconds(showPayCheck.length);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        continueButton.gameObject.SetActive(true);

        LeanTween.value(continueButton.gameObject, (Color c) => continueButton.color = c, continueButton.color, continueButtonIdleColor, continueButtonIdleTime).setLoopPingPong();
    }

    public void LoadMainScene()
    {
        loadingText.SetActive(true);

        StartCoroutine(LoadIntro());
    }

    private IEnumerator LoadIntro()
    {
        yield return new WaitForSeconds(fakeDelayToLoadIntroScene);

        SceneManager.LoadScene("IntroScene");
    }

    private void OnDestroy()
    {
        TimeManager.onTimeOver -= OnTimeOver;
    }
}
