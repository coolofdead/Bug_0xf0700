using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Elevator : MonoBehaviour
{
    public Action onElevatorError;
    public Action onElevatorErrorResolved;
    public Action<int, int> onElevatorMovingToFloor;
    public Action<int> onElevatorReachedFloor;
    
    [field:Header("Current Floor")]
    [field:SerializeField] public int CurrentFloorLevel { get; private set; }

    [Header("Elevator Settings")]
    public Animator elevatorAnimator;
    public AnimationClip elevatorDorosAnimationClip;
    public Transform[] floorsTargets;
    public float timeToMoveToNextFloor = 3f;

    [Header("Error Settings")]
    [Range(0, 100)] public float percentageOfError = 30f;
    public float errorRateInSec = 30f;
    public float timeToRepairErrorInSec = 20f;

    private bool isMoving;
    private bool isOnError;

    private void Awake()
    {
        InvokeRepeating("Error", 0, errorRateInSec);
    }

    private void Error()
    {
        if (UnityEngine.Random.Range(0, 100) > percentageOfError || isMoving || isOnError) return;

        isOnError = true;
        Invoke("FixError", timeToRepairErrorInSec);

        onElevatorError?.Invoke();
    }

    private void FixError()
    {
        isOnError = false;

        onElevatorErrorResolved?.Invoke();
    }

    public void OpenDoors()
    {
        elevatorAnimator.Play("OpenDoors");
    }

    public void CloseDoors()
    {
        elevatorAnimator.Play("CloseDoors");
    }

    public void CallElevator(int floorLevel)
    {
        if (isMoving || isOnError) return;

        if (floorLevel == CurrentFloorLevel)
        {
            OpenDoors();
            return;
        }

        CloseDoors();
        PickFloor(floorLevel);
    }

    public void PickFloor(int floorLevel)
    {
        if (isMoving || isOnError) return;

        StartCoroutine(MoveToFloor(floorLevel));
    }

    private IEnumerator MoveToFloor(int floorLevel) // 1 - 2 - 3
    {
        CloseDoors();
        isMoving = true;

        onElevatorMovingToFloor?.Invoke(CurrentFloorLevel, floorLevel);

        yield return new WaitForSeconds(elevatorDorosAnimationClip.length);

        Vector3 targetPos = floorsTargets[floorLevel - 1].localPosition;
        Vector3 startPos = transform.localPosition;
        float targetTime = timeToMoveToNextFloor * Mathf.Abs(CurrentFloorLevel - floorLevel);
        float currentTime = 0;

        while (currentTime < targetTime)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, currentTime / targetTime);

            currentTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = targetPos;
        CurrentFloorLevel = floorLevel;

        onElevatorReachedFloor?.Invoke(floorLevel);

        isMoving = false;
        OpenDoors();
    }
}
