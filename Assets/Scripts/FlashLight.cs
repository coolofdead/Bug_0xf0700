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

        transform.parent = shooterContainer.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = new Quaternion(0,0,0,0);




        //Rb.useGravity = false;
        //transform.GetComponent<Rigidbody>
    }

    public void Update()
    {
        
    }

}
