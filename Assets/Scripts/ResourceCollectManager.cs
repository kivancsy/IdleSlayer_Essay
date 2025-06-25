using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceCollectManager : MonoBehaviour
{
    public static ResourceCollectManager Instance;

    private Dictionary<string, int> collectedResources = new Dictionary<string, int>();

    public event Action<ResourceType, int> OnResourcesUpdated;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Collect(ResourceType resourceType)
    {
        if (!collectedResources.ContainsKey(resourceType.resourceName))
            collectedResources[resourceType.resourceName] = 0;

        collectedResources[resourceType.resourceName] += resourceType.value;

        OnResourcesUpdated?.Invoke(resourceType, collectedResources[resourceType.resourceName]);
    }
}