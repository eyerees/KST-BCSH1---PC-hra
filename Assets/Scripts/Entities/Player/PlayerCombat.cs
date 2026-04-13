using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackOffset = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 10;
    public float knockbackForce = 5f;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            float lastX = animator.GetFloat("LastInputX");
            float lastY = animator.GetFloat("LastInputY");
            if (lastX == 0f && lastY == 0f) lastY = -1f;

            Vector2 attackDirection = new Vector2(lastX, lastY).normalized;

            if (attackPoint != null)
                attackPoint.localPosition = attackDirection * attackOffset;

            animator.SetBool("isAttacking", true);
            isAttacking = true;
            PerformHit(attackDirection);
        }
    }

    public void PerformHit(Vector2 attackDirection)
    {
        if (attackPoint == null) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position, attackRange, enemyLayers);

        bool hitSomething = false;

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(attackDamage, attackDirection, knockbackForce);
                hitSomething = true;
            }
        }

        if (hitSomething)
        {
            SoundEffectManager.Play("SwordHit", true); 
        }
        else
        {
            SoundEffectManager.Play("SwordMiss", false);
        }
    }
    

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}