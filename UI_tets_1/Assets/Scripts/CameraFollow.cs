using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Update()
    {
        Vector3 desiredPos = new Vector2(target.position.x, target.position.y);
        Vector3 smoothedPos = Vector2.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;      
    }
}
