using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    const float HOURS_TO_DEGREES = 360 / 12, MINUTES_TO_DEGREES = 360 / 60;

    [SerializeField] private TimeManager timeManager;

    public Transform bigClock;
    public Transform smallClock;

    [Header("Feedback")]
    public Image timeLeftFeedback;
    public Gradient timeLeftFeedbackGradient;

    private bool clockIsRunning;
    private float startClockTime;

    private void Start()
    {
        // StartClock();
    }

    private void StartClock()
    {
        clockIsRunning = true;
        startClockTime = Time.time;

        Invoke("EndClock", timeManager.levelDuration);
    }

    private void EndClock()
    {
        clockIsRunning = false;
        timeLeftFeedback.color = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        if (!clockIsRunning) return;

        timeLeftFeedback.color = timeLeftFeedbackGradient.Evaluate((Time.time - startClockTime) / timeManager.levelDuration);
        timeLeftFeedback.fillAmount = 1 - timeManager.GetHour() / TimeManager.HOURS_IN_DAY;
        
        bigClock.rotation = Quaternion.Euler(0, 0, -timeManager.GetMinutes() * MINUTES_TO_DEGREES);
        smallClock.rotation = Quaternion.Euler(0, 0, -timeManager.GetHour() * HOURS_TO_DEGREES);
    }
}
