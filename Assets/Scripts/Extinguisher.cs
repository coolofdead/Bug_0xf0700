using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 10f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.tag == "Fire")
            {
              
                if (Input.GetMouseButtonDown(0))
                {
                    
                }
            }
        }
    }
}
