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

    public bool debugBugWithB = true;

    [Header("Tuto Related")]
    public float newBugAfterTime;

    [Header("Bug Related")]
    public float timeBetweenBugs = 45f;
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
        if (Input.GetKeyDown(KeyCode.B) && debugBugWithB) BugAppear();
    }

    public void StartBugs()
    {
        Invoke("MakeBug", timeBetweenBugs);
    }

    private void MakeBug(int floorLevel)
    {
        TotalOfComputersHacked++;

        var rnd = new System.Random();
        var computersNotBugged = computers.Where((computer) => !computer.IsBugged && computer.FloorLevel == floorLevel).ToList();
        computersNotBugged.Sort((i, c) => rnd.Next());
        var computerToBug = computersNotBugged.First();
        computerToBug.CreateBug();
    }
    public void BugAppear()
    {
        int floorLevelBug = UnityEngine.Random.Range(1, 4);
        MakeBug(floorLevelBug);

        FireHackEvent();
    }

    private void OnComputerBugResolved(Computer computer)
    {
        TotalOfComputersHacked--;
        TotalOfComputersFixed++;

        if (TotalOfComputersFixed == 1) // Tutorial done
        {
            Invoke("BugAppear", newBugAfterTime);
        }
        else if (TotalOfComputersFixed == 2)
        {
            lightEvent.TurnOffAllLights();
            Invoke("BugAppear", newBugAfterTime);
        }
        else
        {
            var percentage = percentageOfHavingTwoBugs.Evaluate(TotalOfComputersFixed);
            int nbBugToMake = UnityEngine.Random.Range(0, 100) < percentage ? 1 : 1 + (int)(TotalOfComputersFixed / addOneToMaxComputersHackPerFix);
            for (int i = 0; i < nbBugToMake; i++)
            {
                Invoke("BugAppear", timeBetweenBugs);
            }
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

    private void OnDestroy()
    {
        Computer.onComputerFix -= OnComputerBugResolved;
    }
}
