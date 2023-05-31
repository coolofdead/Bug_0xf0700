using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPickable : MonoBehaviour, IInteractable
{
    public Rigidbody rb;
    
    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color pickedColor;
    [SerializeField] private Outline outline;

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
}
