using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUp : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;
    private bool isPop = false;
    
    private void Start()
    {
        transform.localScale = Vector2.zero;
    }
    public void Pop()
    {
        if (!isPop)
        {
            transform.LeanScale(Vector2.one, 0.4f).setEaseOutBack();
            sfx.Play();
        }

        isPop = true;
    }
    
    public void Close()
    {
        if (isPop)
            transform.LeanScale(Vector2.zero, 0.4f).setEaseInBack();

        isPop = false;
    }
}
