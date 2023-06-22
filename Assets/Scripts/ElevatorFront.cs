using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevatorFront : MonoBehaviour
{
    private const string elevatorMovingUp = "▲";
    private const string elevatorMovingDown = "▼";

    public int floorLevel;
    public Elevator elevatorLinked;
    public CallElevatorButton callElevatorButton;
    public TextMeshProUGUI floorLevelTMP;
    public Animator floorLevelAnimator;
    public Animator frontDoorAnimator;

    private void Awake()
    {
        elevatorLinked.onElevatorMovingToFloor += OnElevatorStartMoving;
        elevatorLinked.onElevatorReachedFloor += OnElevatorReachedFloor;
    }

    public void OnElevatorStartMoving(int currentLevel, int targetFloorLevel)
    {
        floorLevelTMP.text = targetFloorLevel > currentLevel ? elevatorMovingUp : elevatorMovingDown;
        floorLevelAnimator.Play("ShowElevatorMoving");

        if (targetFloorLevel != floorLevel) frontDoorAnimator.Play("CloseDoors");
    }

    public void OnElevatorReachedFloor(int targetFloorLevel)
    {
        if (targetFloorLevel == floorLevel) frontDoorAnimator.Play("OpenDoors");

        floorLevelTMP.text = targetFloorLevel.ToString();
        floorLevelAnimator.Play("Idle");
        callElevatorButton.ResetColor();
    }

    public void CallElevator()
    {
        if (elevatorLinked.CurrentFloorLevel == floorLevel) frontDoorAnimator.Play("OpenDoors");

        elevatorLinked.CallElevator(floorLevel);
    }

    private void OnDestroy()
    {
        elevatorLinked.onElevatorMovingToFloor -= OnElevatorStartMoving;
        elevatorLinked.onElevatorReachedFloor -= OnElevatorReachedFloor;
    }
}
