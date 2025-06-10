using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] ResourceType resourceType;

    private void Start()
    {
        ResourceManager.Instance.resources.Add(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResourceManager.Instance.RemoveResource(gameObject);
            ResourceCollectManager.Instance.Collect(resourceType);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            ResourceManager.Instance.RemoveResource(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.RemoveResource(gameObject);
    }
}