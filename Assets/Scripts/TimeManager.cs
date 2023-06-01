using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public const int HOURS_IN_DAY = 12, MINUTES_IN_HOUR = 60;

    public float levelDurationInMinutes = 5;
    public float levelDuration => 60f * levelDurationInMinutes;

    private float totalTime = 0;
    private float currentTime = 0;

    void Update()
    {
        totalTime += Time.deltaTime;
        currentTime = totalTime % levelDuration;
    }

    public float GetHour()
    {
        return currentTime * HOURS_IN_DAY / levelDuration;
    }

    public float GetMinutes()
    {
        return (currentTime * HOURS_IN_DAY * MINUTES_IN_HOUR / levelDuration) % MINUTES_IN_HOUR;
    }
}
