using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField]
    private CameraPointType type;
    [Range(1, 10)]
    public float camSpeed = 2;

    public CameraPointType GetCameraPointType()
    {
        Destroy(gameObject, Time.deltaTime * 2);
        return type;
    }
}
