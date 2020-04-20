using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerArm : MonoBehaviour
{
    List<ResourceNode> _nearbyNodes = new List<ResourceNode>();
    float _angle = 0;
    Transform _t;
    Vector3 _shortestDelta = new Vector3();

    private void OnTriggerEnter(Collider other)
    {
        ResourceNode node = other.GetComponent<ResourceNode>();
        if (node != null)
        {
            _nearbyNodes.Add(node);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ResourceNode node = other.GetComponent<ResourceNode>();
        if (node != null)
        {
            _nearbyNodes.Remove(node);
        }
    }

    private void Awake()
    {
        _t = GetComponent<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        _shortestDelta.Set(0, 0, 0);
        for (int i = _nearbyNodes.Count-1; i >= 0; i--)
        {
            if (_nearbyNodes[i] != null)
            {
                ResourceNode node = _nearbyNodes[i];
                Vector3 delta = _t.position - node.transform.position;
                if (i == _nearbyNodes.Count - 1 || delta.magnitude < _shortestDelta.magnitude)
                {
                    _shortestDelta = delta;
                }
                
            }
            else
            {
                _nearbyNodes.RemoveAt(i);
            }
        }
        if (_shortestDelta.magnitude > 0)
        {
            _angle = Mathf.Atan2(_shortestDelta.z, _shortestDelta.x) * Mathf.Rad2Deg;
            _t.rotation = Quaternion.Euler(0, -_angle, 0);
        } else
        {
            _t.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
