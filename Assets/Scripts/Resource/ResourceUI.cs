using System;
using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private Sprite image;
    [SerializeField] private ResourceType resourceType;

    private void Start()
    {
        ResourceCollectManager.Instance.OnResourcesUpdated += UpdateResourceUI;
    }
    
    void UpdateResourceUI(ResourceType resourceType, int amount)
    {
        if (this.resourceType == resourceType)
        {
            resourceText.text = amount.ToString();
            
        }
    }
    private void OnDisable()
    {
        ResourceCollectManager.Instance.OnResourcesUpdated -= UpdateResourceUI;
    }
}