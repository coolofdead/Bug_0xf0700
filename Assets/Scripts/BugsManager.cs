using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

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

    private Computer[] computers;

    private void Awake()
    {
        Instance = this;
        computers = FindObjectsOfType<Computer>();
        Computer.onComputerBugResolve += OnComputerBugResolved;
    }

    public void StartBugs()
    {
        Invoke("MakeBug", timeBetweenBugs);
    }

    private void MakeBug()
    {
        var rnd = new System.Random();
        var computersNotBugged = computers.Where((computer) => !computer.IsBugged).ToList();
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
            MakeBug();
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
        Computer.onComputerBugResolve -= OnComputerBugResolved;
    }
}
