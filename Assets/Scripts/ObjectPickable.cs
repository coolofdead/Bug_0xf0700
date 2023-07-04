using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline), typeof(Collider), typeof(Rigidbody))]
public class ObjectPickable : MonoBehaviour, IInteractable
{
    [field:Header("Handle")]
    [field:SerializeField] public bool ShoudBeHoldInHand { get; private set; }  = false;
    public RigidbodyConstraints onHandConstraints = RigidbodyConstraints.FreezeAll;
    public Vector3 sizeInHand = Vector3.one;
    [field:SerializeField] public Rigidbody Rb;

    [Header("Outline")]
    [SerializeField] protected Color hoverColor;
    [SerializeField] protected Color pickedColor;
    [SerializeField] protected Outline outline;

    [HideInInspector] public bool isPick = false;

    public UnityEvent onPickup;
    public UnityEvent onRelease;

    private RigidbodyConstraints defaultConstraints;

    private Vector3 defaultSize;

    private void Awake()
    {
        if (Rb == null) Rb = GetComponent<Rigidbody>();    
        if (outline == null) outline = GetComponent<Outline>();
        defaultConstraints = Rb.constraints;
        defaultSize = transform.localScale;
    }

    public virtual void Pick()
    {
        outline.OutlineColor = pickedColor;
        EnableOutline();
        isPick = true;

        if (ShoudBeHoldInHand)
        {
            Rb.constraints = onHandConstraints;
            transform.localPosition = Vector3.zero;
            transform.localScale = sizeInHand;
            transform.localRotation = Quaternion.identity;
        }

        onPickup?.Invoke();
    }

    public virtual void Release()
    {
        DisableOutline();
        Rb.velocity = Vector3.zero;
        isPick = false;

        if (ShoudBeHoldInHand)
        {
            Rb.constraints = defaultConstraints;
            transform.localScale = defaultSize;
        }

        onRelease?.Invoke();
    }

    protected void EnableOutline()
    {
        outline.enabled = true;
    }

    protected void DisableOutline()
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
    }

    public bool IsInteractable()
    {
        return true;
    }
}
