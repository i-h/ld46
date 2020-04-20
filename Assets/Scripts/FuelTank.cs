using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank
{
    float _fuel = 0;
    float _maxFuel = -1;
    
    public bool Consume(float amount)
    {
        _fuel -= amount;
        if(_fuel <= 0)
        {
            _fuel = 0;
            return false;
        }
        return true;
    }
    public float AddFuel(float amount)
    {
        float excess = 0;
        if(_maxFuel >= 0 && _fuel + amount > _maxFuel)
        {
            _fuel = _maxFuel;
            excess = _fuel + amount - _maxFuel;
        } else
        {
            _fuel += amount;
        }
        Debug.Log("Current fuel: " + _fuel.ToString("F"));
        return excess;
    }

    public float Refuel(ResourceType type, int amount, float efficiency = 1.0f)
    {
        float convertedFuel = 0;
        switch (type)
        {
            case ResourceType.Crystal:
                convertedFuel = amount * 25;
                break;
            default:
                return convertedFuel;
        }
        return convertedFuel * efficiency;
    }

    public float GetFuelAmount()
    {
        return _fuel;
    }
}
