using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float moveAmount = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float minY = 0f;
    [SerializeField] float maxY = 2f;

    float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * moveSpeed, moveAmount);
        float newY = Mathf.Clamp(startY + pingPong, minY, maxY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }
}