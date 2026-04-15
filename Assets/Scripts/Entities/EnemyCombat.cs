using UnityEngine;

public class EnemyCombat : MonoBehaviour 
{
    public int damage = 1;
    public float attackInterval = 1.0f;
    private float nextAttackTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-damage);
                nextAttackTime = Time.time + attackInterval;
            }
        }
    }
}