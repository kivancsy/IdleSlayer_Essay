using UnityEngine;

public class RotateLantern : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.2f;
    [SerializeField] private float floatFrequency = 1f;
    [SerializeField] private float rotationSpeed = 90f;

    private Vector3 startLocalPos;

    private void Start()
    {
        startLocalPos = transform.localPosition;
    }

    private void Update()
    {
        LanternFloatMove();
    }


    private void LanternFloatMove()
    {
        float offsetY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        float offsetZ = Mathf.Cos(Time.time * floatFrequency) * floatAmplitude;
        float offsetX = Mathf.Cos(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = startLocalPos + new Vector3(offsetX, offsetY, offsetZ);

        //RotateLanternObject();
    }

    private void RotateLanternObject()
    {
        transform.Rotate(
            Vector3.right * (rotationSpeed * Time.deltaTime), Space.Self);
    }
}