using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour 
{
    public int currentHealth = 6;
    public int maxHealth = 6;

    public static event Action OnHealthChanged;

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        OnHealthChanged?.Invoke();
        
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false); 
        }
    }
}