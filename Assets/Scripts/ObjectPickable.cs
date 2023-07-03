using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Outline), typeof(Collider), typeof(Rigidbody))]
public class ObjectPickable : MonoBehaviour, IInteractable
{
    [field:SerializeField] public Rigidbody Rb;
    public bool isPick = false;
    
    [Header("Outline")]
    [SerializeField] protected Color hoverColor;
    [SerializeField] protected Color pickedColor;
    [SerializeField] protected Outline outline;

    private void Awake()
    {
        if (Rb == null) Rb = GetComponent<Rigidbody>();    
        if (outline == null) outline = GetComponent<Outline>();    
    }

    public virtual void Pick()
    {
        outline.OutlineColor = pickedColor;
        EnableOutline();
        isPick = true;
    }

    public void Release()
    {
        DisableOutline();
        isPick = false;
    }

    protected void EnableOutline()
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

    public bool IsInteractable()
    {
        return true;
    }
}
