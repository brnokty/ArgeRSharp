using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runMultiplier = 2f;

    public Animator animator;

    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Hareket girişi al
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveInput = new Vector3(moveX, 0, moveZ).normalized;

        // Hız hesapla
        float currentSpeed = moveInput.magnitude * moveSpeed;

        // Koşma tuşu basılıysa hız artır
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= runMultiplier;
        }

        // Animator parametresine ata
        animator.SetFloat("Speed", currentSpeed/5);

        // Yönü değiştir
        if (moveInput != Vector3.zero)
        {
            transform.forward = moveInput;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
