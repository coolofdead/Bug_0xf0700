using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : ObjectPickable
{
    public override void Pick()
    {
        outline.OutlineColor = pickedColor;
        EnableOutline();
        isPick = true;

        Rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void Release()
    {
        Rb.constraints = RigidbodyConstraints.None;
        DisableOutline();
        isPick = false;
        transform.parent = transform.root;
    }

    public void Update()
    {
        
    }

}
