using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class BugsManager : MonoBehaviour
{
    public static Action onHack;
    public static BugsManager Instance { get; private set; }

    [Header("Tuto Related")]
    public float rushMinNewBugAfterTime;
    public float rushMaxNewBugAfterTime;
    public float chillMinNewBugAfterTime;
    public float chillMaxNewBugAfterTime;
    public float durationOfRushTime = 15f;
    public float durationOfChillTime = 15f;

    private bool isInRush;

    [Header("Bug Related")]
    public float delayBeforeNextBugOnFixFirstBug = 30f;
    public float smallDelayBetweenMutlipleBugs= 6f;
    public float addOneToMaxComputersHackPerFix = 4;
    public AnimationCurve percentageOfHavingTwoBugs;
    public AudioSource audioSource;

    [Header("Bug Effects")]
    public Animator warningCoverUI;
    public Image[] warningCoverBuildingFloors;
    public Color targetColor;
    public float timeToSwapColors = 0.5f;

    [Header("Events")]
    public LightEvent lightEvent;

    private Computer[] computers;
    private bool showWarningOnce = true;

    public int TotalOfComputersHacked { get; private set; }
    public int TotalOfComputersFixed { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        computers = FindObjectsOfType<Computer>();
        Computer.onComputerFix += OnComputerBugResolved;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B)) BugAppear();
    }

    public void BugAppear()
    {
        int floorLevelBug = UnityEngine.Random.Range(1, 4);
        MakeBug(floorLevelBug);

        FireHackEvent();
    }

    private void MakeBug(int floorLevel)
    {
        TotalOfComputersHacked++;

        var rnd = new System.Random();
        var computersNotBugged = computers.Where((computer) => !computer.IsBugged && computer.FloorLevel == floorLevel).ToList();
        var computerToBug = computersNotBugged[UnityEngine.Random.Range(0, computersNotBugged.Count)];
        computerToBug.CreateBug();
    }

    private void OnComputerBugResolved(Computer computer)
    {
        TotalOfComputersHacked--;
        TotalOfComputersFixed++;

        if (TotalOfComputersFixed == 1) // Tutorial done
        {
            StartCoroutine(ThrowBugs());
        }
        else if (TotalOfComputersFixed == 2)
        {
            lightEvent.TurnOffAllLights();
        }
    }

    public void BugAppearOnComputer(Computer hackThisComputer)
    {
        LeanTween.value(warningCoverBuildingFloors[hackThisComputer.FloorLevel - 1].gameObject, (Color c) => { warningCoverBuildingFloors[hackThisComputer.FloorLevel - 1].color = c; }, Color.white, targetColor, timeToSwapColors).setLoopType(LeanTweenType.pingPong);
        hackThisComputer.CreateBug();

        FireHackEvent();
    }

    private void FireHackEvent()
    {
        if (showWarningOnce)
        {
            showWarningOnce = false;
            warningCoverUI.Play("ShowWarning");
            Invoke("HideWarningUI", WarningLight.warningLightDuration * 0.7f);
        }

        audioSource.Play();
        Invoke("StopWarningBugAudio", WarningLight.warningLightDuration);

        onHack?.Invoke();
    }

    private void StopWarningBugAudio()
    {
        audioSource.Stop();
    }

    private void HideWarningUI()
    {
        warningCoverUI.SetTrigger("HideWarning");
    }

    private IEnumerator ThrowBugs()
    {
        yield return new WaitForSeconds(delayBeforeNextBugOnFixFirstBug);

        StartCoroutine(HandleChillTime());

        while (true)
        {
            var percentage = percentageOfHavingTwoBugs.Evaluate(TotalOfComputersFixed);
            int nbBugToMake = UnityEngine.Random.Range(0, 100) < percentage ? 1 : 1 + (int)(TotalOfComputersFixed / addOneToMaxComputersHackPerFix);
            print(nbBugToMake);
            for (int i = 0; i < nbBugToMake; i++)
            {
                BugAppear();

                yield return new WaitForSeconds(smallDelayBetweenMutlipleBugs + WarningLight.warningLightDuration);
            }

            var waitTime = isInRush ? UnityEngine.Random.Range(chillMinNewBugAfterTime, chillMaxNewBugAfterTime) : UnityEngine.Random.Range(rushMinNewBugAfterTime, rushMaxNewBugAfterTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private  IEnumerator HandleChillTime()
    {
        while (true)
        {
            isInRush = false;

            yield return new WaitForSeconds(durationOfChillTime);

            isInRush = true;

            yield return new WaitForSeconds(durationOfRushTime);
        }
    }

    private void OnDestroy()
    {
        Computer.onComputerFix -= OnComputerBugResolved;
    }
}
