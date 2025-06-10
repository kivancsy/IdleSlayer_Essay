using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private Sprite image;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateResourceText(string resourceName)
    {
        resourceText.text = resourceName;
    }
}