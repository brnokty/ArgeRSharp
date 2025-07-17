using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runMultiplier = 2f;

    public Animator animator;

    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 moveDirection;

    private bool isDancing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Dance toggle
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isDancing = !isDancing;
        }

        if (isDancing)
        {
            moveDirection = Vector3.zero;
            animator.SetBool("Dance", true);
        }
        else
        {
            animator.SetBool("Dance", false);

            // Input
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // Kameranın yönüne göre input'u döndür
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            moveDirection = (camForward * vertical + camRight * horizontal).normalized;

            // Speed parametresi
            float currentSpeed = moveDirection.magnitude * moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed *= runMultiplier;

            animator.SetFloat("Speed", currentSpeed/5);

            // Karakter yönü
            if (moveDirection != Vector3.zero)
            {
                transform.forward = moveDirection;
            }
        }
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero && !isDancing)
        {
            float speed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
                speed *= runMultiplier;

            rb.linearVelocity = moveDirection * speed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
