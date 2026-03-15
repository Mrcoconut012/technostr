using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    public Slicer slicer;
    private void OnTriggerExit(Collider other)
    {
        slicer.isTouched = true;
    }
}
