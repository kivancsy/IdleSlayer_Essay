using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float laneChangeSpeed = 10f;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Animator playerAnimator;

    [SerializeField] float slideDuration = 0.7f;
    [SerializeField] float slideColliderHeight = 0.5f;
    [SerializeField] BoxCollider playerCollider;

    int currentLaneIndex = 1;
    float targetX;
    float originalColliderHeight;
    bool isSlide = false;
    private float slideTimer;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetX = lanes[currentLaneIndex];
        originalColliderHeight = playerCollider.size.y;
    }

    private void FixedUpdate()
    {
        HandleLaneMovement();
        HandleSlide();
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

    public void Slide(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y < 0)
        {
            Debug.Log("Slide");
            if (!isSlide)
            {
                isSlide = true;
                slideTimer = 0f;
                playerAnimator.SetTrigger("isSlide");
                playerCollider.size = new Vector3(playerCollider.size.x, slideColliderHeight, playerCollider.size.z);
            }
        }
    }

    void HandleSlide()
    {
        if (isSlide)
        {
            slideTimer += Time.fixedDeltaTime;
            if (slideTimer >= slideDuration)
            {
                isSlide = false;
                playerCollider.size = new Vector3(playerCollider.size.x, originalColliderHeight, playerCollider.size.z);
            }
        }
    }

    void HandleLaneMovement()
    {
        Vector3 currentPosition = rb.position;
        Vector3 targetPosition = new Vector3(targetX, currentPosition.y, currentPosition.z);
        Vector3 newPosition =
            Vector3.MoveTowards(currentPosition, targetPosition, laneChangeSpeed * Time.fixedDeltaTime);
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