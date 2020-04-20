using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldUIManager : MonoBehaviour
{
    public static WorldUIManager Instance;

    Dictionary<ResourceType, ResourceCounter> _counters = new Dictionary<ResourceType, ResourceCounter>((int)ResourceType.Count);
    

    private void Awake()
    {
        Instance = this;
        foreach (ResourceCounter counter in GetComponentsInChildren<ResourceCounter>())
        {
            _counters.Add(counter.Type, counter);
            counter.ResourceText.text = "0";
        }
    }

    public void ResourceChanged(ResourceType type, int amount)
    {
        _counters[type].ResourceText.text = amount.ToString();
    }
}
