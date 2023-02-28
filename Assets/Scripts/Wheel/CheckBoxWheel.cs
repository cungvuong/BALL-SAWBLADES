using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxWheel : MonoBehaviour
{
    public void DestrouyParentWheel()
    {
        transform.parent.GetComponent<Wheel>().HandleHideWheel();
    }
}
