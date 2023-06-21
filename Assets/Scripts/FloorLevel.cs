using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLevel : MonoBehaviour
{
    public int floorLevel;

    void Start()
    {
        var computersFloor = GetComponentsInChildren<Computer>();
        foreach (var computerFloor in computersFloor)
        {
            computerFloor.FloorLevel = floorLevel;
        }
    }
}
