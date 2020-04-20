using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { None, Crystal, Count}
public class ResourceStorage
{
    int[] _storage = new int[(int)ResourceType.Count];

    public void AddResource(ResourceType type, int amount)
    {
        AddResource((int)type, amount);
    }
    public void AddResource(int type, int amount)
    {
        _storage[type] += amount;
    }
    public int TakeResource(ResourceType type, int amount)
    {
        int returnedAmount = amount;
        int available = _storage[(int)type];
        if (available < amount)
        {
            returnedAmount = available;
        }

        _storage[(int)type] -= returnedAmount;

        return returnedAmount;
    }

    public int TakeAll(ResourceType type)
    {
        return TakeAll((int)type);
    }
    public int TakeAll(int type)
    {
        int returnedAmount = _storage[type];
        _storage[type] = 0;
        return returnedAmount;
    }

    public int CheckResource(ResourceType type)
    {
        return _storage[(int)type];
    }

    public void Dump(ResourceStorage other)
    {
        for(int i = 0; i < (int)ResourceType.Count; i++)
        {
            AddResource(i, other.TakeAll(i));
        }
    }

}
