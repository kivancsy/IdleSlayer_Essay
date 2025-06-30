using UnityEngine;

[CreateAssetMenu(menuName = "Resources/ResourceType")]
public class ResourceType : ScriptableObject
{
    public string resourceName;
    public GameObject gameObject;
    public int value;
}