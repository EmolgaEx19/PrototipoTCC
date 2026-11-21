using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpForce = 10f;
    private bool isGrounded;

    public int maxHealth = 100;
    public int health;
    public bool isAlive = true;

    [Header("Damage Feedback")]
    [SerializeField] private DamageFlashUI damageFlashUI;

    [Header("HUD")]
    [SerializeField] private HealthBarUI healthBarUI;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ApplyHealthBonus();
        // Inicializa a vida do jogador
        maxHealth = Mathf.Max(1, maxHealth);
        health = maxHealth;
        isAlive = true;

        UpdateHealthUI();
    }

    private void Update()
    {
        if (!isAlive)
            return;

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

    private void ApplyHealthBonus()
    {
        if (GameManager.Instance == null)
            return;

        OutfitStats stats =
            GameManager.Instance.GetTotalOutfitStats();

        if (stats.health <= 0)
            return;

        int bonusHealth = Mathf.RoundToInt(
            Mathf.Max(0f, stats.health)
        );

        maxHealth += bonusHealth;

        /*Debug.Log(
            "Vida máxima com bônus: " + maxHealth
        );*/
    }


    public void TakeDamage(int damage)
    {
        if (!isAlive || damage <= 0)
            return;

        health -= damage;

        if (damageFlashUI != null)
        {
            damageFlashUI.Flash();
        }

        UpdateHealthUI();

        if (health <= 0) { 
            health = 0;
            Die();
        }

        //Debug.Log("Vida do jogador: " + health);
    }

    private void UpdateHealthUI()
    {
        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealth(health, maxHealth);
        }
    }

    private void Die()
    {
        isAlive = false;

        RunnerGameManager runnerManager =
            FindFirstObjectByType<RunnerGameManager>();

        if (runnerManager != null)
        {
            runnerManager.FinalizeRun();
        }
    }
}