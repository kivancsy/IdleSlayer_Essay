using System;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceText;

    public void UpdateHealth(float current)
    {
        resourceText.text = current.ToString();
    }
}