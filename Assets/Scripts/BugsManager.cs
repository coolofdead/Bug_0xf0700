using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class BugsManager : MonoBehaviour
{
    public int totalComputersBugged = 8;
    public int totalComputersBugFixed { get; private set; }

    public TextMeshProUGUI totalComputersFixedTMP;

    private Computer[] computers;

    private void Awake()
    {
        computers = FindObjectsOfType<Computer>();
        Computer.onComputerBugResolve += OnComputerBugResolved;

        InitBugs();
    }

    private void InitBugs()
    {
        var rnd = new System.Random();
        computers.ToList().Sort((i, c) => rnd.Next());
        for (int i = 0; i < totalComputersBugged; i++)
        {
            computers[i].CreateBug();
        }
    }

    private void OnComputerBugResolved()
    {
        totalComputersBugFixed++;
        totalComputersFixedTMP.text = $"{totalComputersBugFixed} / {totalComputersBugged}";

        if (totalComputersBugFixed != totalComputersBugged) return;

        print("all bugs resolved");
    }

    private void OnDestroy()
    {
        Computer.onComputerBugResolve -= OnComputerBugResolved;
    }
}
