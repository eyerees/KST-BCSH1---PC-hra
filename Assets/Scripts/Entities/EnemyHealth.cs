using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float knockbackDuration = 0.15f;
    public bool IsKnockedBack { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void TakeDamage(int damage, Vector2 direction, float force)
    {
        currentHealth -= damage;
        StartCoroutine(FlashColor());

        if (currentHealth <= 0) { Die(); return; }

        if (force > 0f && rb != null && !IsKnockedBack)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
            Invoke(nameof(EndKnockback), knockbackDuration);
            IsKnockedBack = true;
        }
    }

    private IEnumerator FlashColor()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
        }
    }

    public void TakeDamage(int damage) => TakeDamage(damage, Vector2.zero, 0f);
    void EndKnockback() { IsKnockedBack = false; if (rb != null) rb.linearVelocity = Vector2.zero; }
    void Die() => Destroy(gameObject);
}