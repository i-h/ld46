using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{

    ResourceType _resourceType = ResourceType.Crystal;
    int _resourceValue = 2;
    float _durability = 20.0f;

    public void Mine(Miner miner)
    {
        _durability -= miner.Strength;
        if(_durability <= 0)
        {
            miner.Collect(_resourceType, _resourceValue);
            Dispose(miner);
        }
    }

    void Dispose(Miner to)
    {
        /*
        for(int i = 0; i < _resourceValue; i++)
        {
            ResourcePiece piece = Instantiate<ResourcePiece>(PiecePrefab);
            Vector3 launchDir = Random.onUnitSphere;
            launchDir.y = Mathf.Abs(launchDir.y);
            piece.Velocity = launchDir;
            piece.Target = to;

        }
        */

        Destroy(gameObject);
    }
}
