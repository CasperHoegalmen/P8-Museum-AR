using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{ 
    void Update()
    {
        BillboardObject();
    }

    private void BillboardObject()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
