using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform lookPoint;
    [SerializeField]
    private LayerMask ignoreMask;
    [SerializeField, Range(1, 10)]
    private float camSpeed;
    
    [SerializeField]
    [Tooltip("LeftUpNear = 0, ForwardUpNear = 1, RightUpNear = 2, LeftNear = 3, ForwardNear = 4, RightNear = 5, LeftUpFar = 6, ForwardUpFar = 7," +
        "RightUpFar = 8, LeftFar = 9, ForwardFar = 10, RightFar = 11 ")]
    private List<Transform> cameraPoints;

    private Transform targetBufer;
    private bool move;
    private float timer;

    public void SetCamLookPoint(Transform target)
    {
        lookPoint = target;
    }
    public void SetCamTarget(CameraPointType type)
    {
        cam.parent = null;
        targetBufer = cameraPoints[(int)type];
        move = true;
    }

    private void Start()
    {
        SetCamTarget(CameraPointType.ForwardNear);
    }

    private void LateUpdate()
    {
        MoveCamToTarget();
        RayCheck();
        cam.LookAt(lookPoint.position + Vector3.up);
    }

    private void RayCheck()
    {
        Vector3 dir = cam.position - (lookPoint.position + Vector3.up);
        if (Physics.Raycast((lookPoint.position + Vector3.up), dir, out RaycastHit hit, dir.magnitude, ~ignoreMask))
        {
            cam.position = hit.point + hit.normal * 0.3f;
            timer = 0;
        }
        else
        {
            if(timer >= 0 && timer < 1)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = -1;
                move = true;
            }

        }
    }

    private void MoveCamToTarget()
    {
        if(move)
        {
            Vector3 dir = targetBufer.position - cam.position;

            if (dir.magnitude < camSpeed * 2 * Time.deltaTime)
            {
                cam.parent = targetBufer;
                move = false;
            }
            else
            {
                cam.position += dir.normalized * camSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("CameraChanger"))
        {
            CameraChanger changer = other.GetComponent<CameraChanger>();
            if(changer != null)
            {
                camSpeed = changer.camSpeed;
                SetCamTarget(changer.GetCameraPointType());
            }
        }
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < cameraPoints.Count; i++)
        {
            Transform bufer = cameraPoints[i];
            if (bufer != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(bufer.position + bufer.right * 0.2f + bufer.up * 0.1f, bufer.position + bufer.right * 0.2f - bufer.up * 0.1f);
                Gizmos.DrawLine(bufer.position + bufer.right * 0.2f - bufer.up * 0.1f, bufer.position - bufer.right * 0.2f - bufer.up * 0.1f);
                Gizmos.DrawLine(bufer.position - bufer.right * 0.2f - bufer.up * 0.1f, bufer.position - bufer.right * 0.2f + bufer.up * 0.1f);
                Gizmos.DrawLine(bufer.position - bufer.right * 0.2f + bufer.up * 0.1f, bufer.position + bufer.right * 0.2f + bufer.up * 0.1f);

                Gizmos.DrawLine(bufer.position + bufer.right * 0.2f + bufer.up * 0.1f, bufer.position + bufer.right * 0.4f + bufer.up * 0.2f + bufer.forward * 0.5f);
                Gizmos.DrawLine(bufer.position + bufer.right * 0.2f - bufer.up * 0.1f, bufer.position + bufer.right * 0.4f - bufer.up * 0.2f + bufer.forward * 0.5f);
                Gizmos.DrawLine(bufer.position - bufer.right * 0.2f - bufer.up * 0.1f, bufer.position - bufer.right * 0.4f - bufer.up * 0.2f + bufer.forward * 0.5f);
                Gizmos.DrawLine(bufer.position - bufer.right * 0.2f + bufer.up * 0.1f, bufer.position - bufer.right * 0.4f + bufer.up * 0.2f + bufer.forward * 0.5f);

                Gizmos.DrawLine(bufer.position + bufer.right * 0.4f + bufer.up * 0.2f + bufer.forward * 0.5f,
                    bufer.position + bufer.right * 0.4f - bufer.up * 0.2f + bufer.forward * 0.5f);
                Gizmos.DrawLine(bufer.position + bufer.right * 0.4f - bufer.up * 0.2f + bufer.forward * 0.5f,
                    bufer.position - bufer.right * 0.4f - bufer.up * 0.2f + bufer.forward * 0.5f);
                Gizmos.DrawLine(bufer.position - bufer.right * 0.4f - bufer.up * 0.2f + bufer.forward * 0.5f,
                    bufer.position - bufer.right * 0.4f + bufer.up * 0.2f + bufer.forward * 0.5f);
                Gizmos.DrawLine(bufer.position - bufer.right * 0.4f + bufer.up * 0.2f + bufer.forward * 0.5f,
                    bufer.position + bufer.right * 0.4f + bufer.up * 0.2f + bufer.forward * 0.5f);

            }
        }
    }
#endif
}

public enum CameraPointType
{
    LeftUpNear = 0,
    ForwardUpNear = 1,
    RightUpNear = 2,

    LeftNear = 3,
    ForwardNear = 4,
    RightNear = 5,

    LeftUpFar = 6,
    ForwardUpFar = 7,
    RightUpFar = 8,

    LeftFar = 9,
    ForwardFar = 10,
    RightFar = 11
}
