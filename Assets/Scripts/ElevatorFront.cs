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

    private void Awake()
    {
        elevatorLinked.onElevatorMovingToFloor += OnElevatorStartMoving;
        elevatorLinked.onElevatorReachedFloor += OnElevatorReachedFloor;
    }

    public void OnElevatorStartMoving(int currentLevel, int targetFloorLevel)
    {
        floorLevelTMP.text = targetFloorLevel > currentLevel ? elevatorMovingUp : elevatorMovingDown;
        floorLevelAnimator.Play("ShowElevatorMoving");
    }

    public void OnElevatorReachedFloor(int targetFloorLevel)
    {
        floorLevelTMP.text = targetFloorLevel.ToString();
        floorLevelAnimator.Play("Idle");
        callElevatorButton.ResetColor();
    }

    public void CallElevator()
    {
        elevatorLinked.CallElevator(floorLevel);
    }

    private void OnDestroy()
    {
        elevatorLinked.onElevatorMovingToFloor -= OnElevatorStartMoving;
        elevatorLinked.onElevatorReachedFloor -= OnElevatorReachedFloor;
    }
}
