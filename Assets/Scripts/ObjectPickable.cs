using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Outline), typeof(Collider), typeof(Rigidbody))]
public class ObjectPickable : MonoBehaviour, IInteractable
{
    [field:SerializeField] public Rigidbody Rb;
    
    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color pickedColor;
    [SerializeField] private Outline outline;

    private void Awake()
    {
        if (Rb == null) Rb = GetComponent<Rigidbody>();    
        if (outline == null) outline = GetComponent<Outline>();    
    }

    public void Pick()
    {
        outline.OutlineColor = pickedColor;
        EnableOutline();
    }

    public void Release()
    {
        DisableOutline();
    }

    private void EnableOutline()
    {
        outline.enabled = true;
    }

    private void DisableOutline()
    {
        outline.enabled = false;
    }

    public void Hover()
    {
        outline.OutlineColor = hoverColor;
        EnableOutline();
    }

    public void ExitHover()
    {
        outline.OutlineColor = pickedColor;
        DisableOutline();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
