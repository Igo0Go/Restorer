using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMascChanger : MonoBehaviour
{
    [SerializeField]
    private LayerMask foolMask;
    [SerializeField]
    private Camera cam;

    public void ChangeMask()
    {
        cam.cullingMask = foolMask;
    }
}
