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

    [Header("Bug Related")]
    public float timeBetweenBugs = 45f;
    [Range(0, 100)]
    public float percentageOfHavingTwoBugs = 70;

    [Header("Bug Effects")]
    public GameObject warningCoverUI;
    public Image[] warningCoverBuildingFloors;
    public Color targetColor;
    public float timeToSwapColors = 0.5f;

    private Computer[] computers;

    private void Awake()
    {
        Instance = this;
        computers = FindObjectsOfType<Computer>();
        RansomWare.onComputerBugResolve += OnComputerBugResolved;
    }

    public void StartBugs()
    {
        Invoke("MakeBug", timeBetweenBugs);
    }

    private void MakeBug(int floorLevel)
    {
        var rnd = new System.Random();
        var computersNotBugged = computers.Where((computer) => !computer.IsBugged && computer.FloorLevel == floorLevel).ToList();
        computersNotBugged.Sort((i, c) => rnd.Next());
        var computerToBug = computersNotBugged.First();
        computerToBug.CreateBug();
    }

    private void OnComputerBugResolved()
    {
        BugAppear();
    }

    public void BugAppear()
    {
        int nbBugToMake = UnityEngine.Random.Range(0, 100) < percentageOfHavingTwoBugs ? 1 : 2;

        for (int i = 0; i < nbBugToMake; i++)
        {
            int floorLevelBug = UnityEngine.Random.Range(1, 4);
            LeanTween.value(warningCoverBuildingFloors[i].gameObject, (Color c) => { warningCoverBuildingFloors[i].color = c; }, Color.white, targetColor, timeToSwapColors).setLoopType(LeanTweenType.pingPong);
            MakeBug(floorLevelBug);

        }

        warningCoverUI.SetActive(true);
        onHack?.Invoke();

        Invoke("HideWarningUI", 16);
    }

    private void HideWarningUI()
    {
        warningCoverUI.SetActive(false);
    }

    private void OnDestroy()
    {
        RansomWare.onComputerBugResolve -= OnComputerBugResolved;
    }
}
