using UnityEngine;
using System;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
    public int currentHealth = 6;
    public int maxHealth = 6;
    
    public Color flashColor = Color.red;
    public float flashDuration = 0.15f;  

    public float iFrameDuration = 1.0f;
    private bool isInvulnerable = false;

    private SpriteRenderer spriteRenderer;
    public static event Action OnHealthChanged;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0 && isInvulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChanged?.Invoke();
        
        if (amount < 0) 
        {
            StartCoroutine(FlashRed());
            if (currentHealth > 0) StartCoroutine(TriggerIFrames());
        }

        if(currentHealth <= 0) gameObject.SetActive(false);
    }

    private IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
        }
    }

    private IEnumerator TriggerIFrames()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(iFrameDuration);
        isInvulnerable = false;
    }
}