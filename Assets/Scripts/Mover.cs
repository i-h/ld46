using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MoveValues))]
public class Mover : MonoBehaviour
{
    Rigidbody _rb;
    Transform _t;
    MoveValues _mv;

    Vector3 _movement;

    public bool UsePlayerFuel = false;
    public float FuelConsumption = 6.0f;
    FuelTank tank;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
        _mv = GetComponent<MoveValues>();
    }
    private void Start()
    {
        tank = PlayerData.Instance.GetCurrentVehicleFuelTank();
    }
    private void FixedUpdate()
    {
        if (_movement.magnitude > 0)
        {
            float angle = Mathf.Atan2(_movement.z, _movement.x) * Mathf.Rad2Deg;
            _t.rotation = Quaternion.Euler(0, -angle+90, 0);
        }


        if (_movement.magnitude > _mv.MaxSpeed * Time.fixedDeltaTime)
        {
            _movement = _movement.normalized * _mv.MaxSpeed * Time.fixedDeltaTime;
        }
        if (!UsePlayerFuel || tank.Consume(FuelConsumption * Time.fixedDeltaTime * _movement.magnitude))
        {
            _rb.MovePosition(_t.position + _movement);
        }

        _movement.Set(0, 0, 0);
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        _movement += dir;
    }
}
