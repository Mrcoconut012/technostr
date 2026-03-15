using UnityEngine;

public class NewSliceListener : MonoBehaviour
{
    public NewSlicer slicer;
    private void OnTriggerEnter(Collider other)
    {
        slicer.isTouched = true;
    }
}
