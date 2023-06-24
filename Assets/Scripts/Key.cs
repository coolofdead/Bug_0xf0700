using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public enum KeyType
    {
        Red,
        Blue,
        Green,
        Yellow
    }

    [SerializeField] private KeyType keyType;
    [SerializeField] private KeyHolder keyHolder;

    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Outline outline;


    public KeyType GetKeyType()
    {
        return keyType;
    }

    public void Interact()
    {
        keyHolder.AddKey(GetKeyType());
        Destroy(gameObject);
    }

    public void Hover()
    {
        outline.OutlineColor = hoverColor;
        outline.enabled = true;
    }

    public void ExitHover()
    {
        outline.enabled = false;
    }
}