using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth; 
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public GameObject emptyHeartPrefab;

    private void OnEnable() => PlayerHealth.OnHealthChanged += UpdateHearts;
    private void OnDisable() => PlayerHealth.OnHealthChanged -= UpdateHearts;

    private void Start() => UpdateHearts();

    void UpdateHearts()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);

        int fullHearts = playerHealth.currentHealth / 2;
        bool hasHalfHeart = playerHealth.currentHealth % 2 != 0;
        int emptyHearts = (playerHealth.maxHealth / 2) - fullHearts - (hasHalfHeart ? 1 : 0);

        for (int i = 0; i < fullHearts; i++) Instantiate(fullHeartPrefab, transform);
        if (hasHalfHeart) Instantiate(halfHeartPrefab, transform);
        for (int i = 0; i < emptyHearts; i++) Instantiate(emptyHeartPrefab, transform);
    }
}