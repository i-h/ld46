using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform TargetObject;
    public Vector3 Offset;

    Transform _t;

    private void Awake()
    {
        _t = GetComponent<Transform>();
    }

    private void Start()
    {
        if (TargetObject == null) enabled = false;
    }
    void FixedUpdate()
    {
        _t.position = TargetObject.position + Offset;
    }
}
