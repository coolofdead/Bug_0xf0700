using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float rotSpeed = 1f;
    public Vector3 doorPosOpen, doorPosClose;
    public bool isOpen = false;
    public bool isLock = false;
    public AudioClip sfx;

    private void Update()
    {
        Open();
    }
    public void Open()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(doorPosOpen), rotSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(doorPosClose), rotSpeed * Time.deltaTime);
        }

    }
}
