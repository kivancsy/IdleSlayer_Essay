using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float laneChangeSpeed = 10f;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Animator playerAnimator;

    int currentLaneIndex = 1;
    float targetX;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetX = lanes[currentLaneIndex];
    }

    private void FixedUpdate()
    {
        HandleLaneMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input.x < 0)
            MoveLeft();
        else if (input.x > 0)
            MoveRight();
    }

    void MoveLeft()
    {
        if (currentLaneIndex > 0)
        {
            currentLaneIndex--;
            targetX = lanes[currentLaneIndex];
            playerAnimator.SetTrigger("jump");
        }
    }

    void MoveRight()
    {
        if (currentLaneIndex < lanes.Length - 1)
        {
            currentLaneIndex++;
            targetX = lanes[currentLaneIndex];
            playerAnimator.SetTrigger("jump");
        }
    }

    void HandleLaneMovement()
    {
        Vector3 currentPosition = rb.position;
        Vector3 targetPosition = new Vector3(targetX, currentPosition.y, currentPosition.z);
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, laneChangeSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyMelee"))
        {
            playerAttack.Attack(other.gameObject);
        }
    }
}