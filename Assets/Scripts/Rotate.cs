using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotateAxe = Vector3.up;
    public float rotateSpeed = 5;
    public Space space = Space.Self;

    private void Update()
    {
        transform.Rotate(rotateAxe * rotateSpeed * Time.deltaTime, space);
    }
}
