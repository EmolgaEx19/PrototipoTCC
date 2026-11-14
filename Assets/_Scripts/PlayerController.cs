using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpForce = 10f;
    public int health = 100;
    public bool isAlive = true;

    private bool isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
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

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Vida do jogador: " + health);

        if (health <= 0) { 
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        if (!isAlive)
            return;

        isAlive = false;

        RunnerGameManager runnerManager =
            FindFirstObjectByType<RunnerGameManager>();

        if (runnerManager != null)
        {
            runnerManager.FinalizeRun();
        }
    }
}