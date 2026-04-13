using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    private Rigidbody2D rb;
    public float knockbackDuration = 0.15f;
    public bool IsKnockedBack { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        TakeDamage(damage, Vector2.zero, 0f);
    }

    public void TakeDamage(int damage, Vector2 direction, float force)
    {
        Debug.Log($"TakeDamage called — damage:{damage} direction:{direction} force:{force} rb:{rb}");

        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        if (force > 0f && rb != null && !IsKnockedBack)
        {
            Debug.Log("Applying knockback!");
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
            Invoke(nameof(EndKnockback), knockbackDuration);
            IsKnockedBack = true;
        }
        else
        {
            Debug.Log($"Knockback skipped — force:{force} rb:{rb} isKnockedBack:{IsKnockedBack}");
        }
    }

    void EndKnockback()
    {
        IsKnockedBack = false;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}