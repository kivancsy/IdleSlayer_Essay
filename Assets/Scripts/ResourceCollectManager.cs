using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectManager : MonoBehaviour
{
    public static ResourceCollectManager Instance;

    private Dictionary<string, int> collectedResources = new Dictionary<string, int>();

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
        Debug.Log($"Collected {resourceType.resourceName}: {collectedResources[resourceType.resourceName]}");

        UpdateAllResourcesText();
    }
    

    private void UpdateAllResourcesText()
    {
        int total = 0;
        foreach (var resource in collectedResources)
        {
            total += resource.Value;
        }

        UIManager.Instance.UpdateResourceText(total.ToString());
    }
}