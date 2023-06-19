using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator elevatorAnimator;
    public AnimationClip elevatorDorosAnimationClip;
    public Transform[] floorsTargets;
    public float timeToMoveToNextFloor = 3f;

    private int currentFloorLevel = 1;
    private bool isMoving;

    public void OpenDoors()
    {
        elevatorAnimator.Play("OpenDoors");
    }

    public void CloseDoors()
    {
        elevatorAnimator.Play("CloseDoors");
    }

    public void PickFloor(int floorLevel)
    {
        if (currentFloorLevel == floorLevel || isMoving) return;

        StartCoroutine(MoveToFloor(floorLevel));
    }

    private IEnumerator MoveToFloor(int floorLevel) // 1 - 2 - 3
    {
        CloseDoors();
        isMoving = true;

        yield return new WaitForSeconds(elevatorDorosAnimationClip.length);

        Vector3 targetPos = floorsTargets[floorLevel - 1].localPosition;
        Vector3 startPos = transform.localPosition;
        float targetTime = timeToMoveToNextFloor * Mathf.Abs(currentFloorLevel - floorLevel);
        float currentTime = 0;

        while (currentTime < targetTime)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, currentTime / targetTime);

            currentTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = targetPos;
        currentFloorLevel = floorLevel;

        isMoving = false;
        OpenDoors();
    }
}
