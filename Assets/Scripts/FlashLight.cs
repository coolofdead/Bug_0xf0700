using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : ObjectPickable
{

    //[Header("Outline")]
    //[SerializeField] private Color hoverColor;
    //[SerializeField] private Outline outline;

    //private bool isUsed = false;
    //private bool isInteract = false;

    [SerializeField] private Transform shooterContainer;

    public override void Pick()
    {
        outline.OutlineColor = pickedColor;
        EnableOutline();
        isPick = true;

        Rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = shooterContainer.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = new Quaternion(0,0,0,0);

        if (TryGetComponent<Extinguisher>(out var extinguisher))
        {
            transform.Rotate(-90, 0, 0);
        }


        //Rb.useGravity = false;
        //transform.GetComponent<Rigidbody>
    }

    public override void Release()
    {
        Rb.constraints = RigidbodyConstraints.None;
        DisableOutline();
        isPick = false;
        transform.SetParent(null);
    }

    public void Update()
    {
        
    }

}
