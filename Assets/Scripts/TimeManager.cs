using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public static Action<TimeManager> onTimeStart;
    public static Action onTimeOver;

    public const int HOURS_IN_DAY = 12, MINUTES_IN_HOUR = 60;

    public float levelDurationInMinutes = 5;
    public float levelDuration => 60f * levelDurationInMinutes;

    private float totalTime = 0;
    private float currentTime = 0;
    private bool startTime;

    private void Awake()
    {
        Instance = this;
    }

    public void StartTime()
    {
        startTime = true;
        onTimeStart?.Invoke(this);
    }

    private void Update()
    {
        if (!startTime) return;

        totalTime += Time.deltaTime;
        currentTime = totalTime % levelDuration;

        if (totalTime >= levelDuration)
        {
            startTime = false;
            onTimeOver?.Invoke();
        }
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
