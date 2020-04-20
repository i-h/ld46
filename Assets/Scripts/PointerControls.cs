using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(MoveValues))]
public class PointerControls : MonoBehaviour
{
    Mover _m;
    Transform _t;
    MoveValues _mv;

    Vector3 _clickPos;
    Ray _viewRay;

    public LayerMask _clickMask;
    private void Awake()
    {
        _m = GetComponent<Mover>();
        _t = GetComponent<Transform>();
        _mv = GetComponent<MoveValues>();
    }
    private void Start()
    {
        _clickMask = LayerMask.GetMask("Ground");
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateClickPosition();

        }
    }

    void UpdateClickPosition()
    {
        _viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        _clickMask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(_viewRay, out RaycastHit hit, float.MaxValue, _clickMask))
        {
            _clickPos = hit.point;

            _m.Move(_clickPos - _t.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_clickPos, 1.0f);
    }
}
