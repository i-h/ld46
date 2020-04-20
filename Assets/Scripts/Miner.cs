using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public float Strength = 5;
    ResourceStorage _resourceStorage = new ResourceStorage();
    public ResourceStorage GetStorage() { return _resourceStorage; }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            ResourceNode node = other.GetComponent<ResourceNode>();
            node.Mine(this);
        }
    }

    public void Collect(ResourceType type, int amount)
    {
        _resourceStorage.AddResource(type, amount);
        WorldUIManager.Instance.ResourceChanged(type, _resourceStorage.CheckResource(type));
    }
}
