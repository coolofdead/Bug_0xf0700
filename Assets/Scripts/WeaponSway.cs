using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smmoth;
    [SerializeField] private float swayMultiplier;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Mouse.current.delta.x.ReadValue() * swayMultiplier;
        float mouseY = Mouse.current.delta.x.ReadValue() * swayMultiplier;

        // Calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // Rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smmoth);

        //Vector3 finalSway = new Vector3(mouseY, mouseX, 0);

        //transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(finalSway), Time.deltaTime * smmoth);
    }
}