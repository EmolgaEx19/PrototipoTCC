using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpForce = 10f;
    private bool isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(
            rb.velocity.x,
            0f
        );

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}