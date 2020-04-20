using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeSpawner : MonoBehaviour
{
    public ResourceNode[] NodePrefabs;
    public int MinNodes = 1;
    public int MaxNodes = 6;
    public float NodeDensity = 0.8f;

    void Start()
    {
        int nodes = Random.Range(MinNodes, MaxNodes + 1);
        for(int i = 0; i < nodes; i++)
        {
            ResourceNode node = Instantiate<ResourceNode>(NodePrefabs[Random.Range(0, NodePrefabs.Length)]);
            Vector3 nodePosition = Random.insideUnitSphere * NodeDensity;
            nodePosition.y = 0;
            node.transform.position = transform.position + nodePosition;
            node.transform.rotation = Quaternion.Euler(0, Random.value * 360, 0);

        }

        Destroy(gameObject);
    }
}
