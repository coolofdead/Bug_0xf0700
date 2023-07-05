using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BigBugScreen : MonoBehaviour
{
    public TextMeshProUGUI[] totalComputerBuggedByFloorTMP;
    public Image[] floorsParent;
    public Color targetColor;
    public float timeToSwapColors = 0.5f;

    private Dictionary<int, int> bugsByFloor;

    private void Start()
    {
        Computer.onComputerHack += OnComputedHack;
        Computer.onComputerFix += OnComputerFix;

        bugsByFloor = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 } };
    }

    private void OnComputedHack(Computer computer)
    {
        LeanTween.value(
            floorsParent[computer.FloorLevel - 1].gameObject, 
            (Color c) => { floorsParent[computer.FloorLevel - 1].color = c; }, 
            Color.white,
            targetColor,
            timeToSwapColors)
            .setLoopType(LeanTweenType.pingPong
            );

        bugsByFloor[computer.FloorLevel] += 1;
        totalComputerBuggedByFloorTMP[computer.FloorLevel - 1].text = bugsByFloor[computer.FloorLevel].ToString();
    }

    private void OnComputerFix(Computer computer)
    {
        LeanTween.cancel(floorsParent[computer.FloorLevel - 1].gameObject);
        LeanTween.value(
            floorsParent[computer.FloorLevel - 1].gameObject,
            (Color c) => { floorsParent[computer.FloorLevel - 1].color = c; }, 
            floorsParent[computer.FloorLevel - 1].color, Color.white, timeToSwapColors
        );

        bugsByFloor[computer.FloorLevel] -= 1;
        if (bugsByFloor[computer.FloorLevel] < 0) bugsByFloor[computer.FloorLevel] = 0;
        totalComputerBuggedByFloorTMP[computer.FloorLevel - 1].text = bugsByFloor[computer.FloorLevel].ToString();
    }

    private void OnDestroy()
    {
        Computer.onComputerHack -= OnComputedHack;
        Computer.onComputerFix -= OnComputerFix;
    }
}
