using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    private void Awake()
    {
        TimeManager.onTimeOver += OnTimeOver;
    }

    private void OnTimeOver()
    {

    }

    private void OnDestroy()
    {
        TimeManager.onTimeOver -= OnTimeOver;
    }
}
