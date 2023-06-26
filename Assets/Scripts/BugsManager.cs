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

    [Header("Bug Related")]
    public float timeBetweenBugs = 45f;
    [Range(0, 100)]
    public float percentageOfHavingTwoBugs = 70;

    [Header("Bug Effects")]
    public Animator warningCoverUI;
    public Image[] warningCoverBuildingFloors;
    public Color targetColor;
    public float timeToSwapColors = 0.5f;

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

    private void OnComputerBugResolved(Computer computer)
    {
        TotalOfComputersHacked--;
        TotalOfComputersFixed++;

        //Invoke("BugAppear", timeBetweenBugs);
    }

    public void BugAppear()
    {
        int nbBugToMake = UnityEngine.Random.Range(0, 100) < percentageOfHavingTwoBugs ? 1 : 2;
        for (int i = 0; i < nbBugToMake; i++)
        {
            int floorLevelBug = UnityEngine.Random.Range(1, 4);
            MakeBug(floorLevelBug);
        }

        FireHackEvent();
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
            Invoke("HideWarningUI", 8);
        }

        onHack?.Invoke();
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
