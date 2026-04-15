using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;

    public float attackRange = 0.5f;
    public float attackOffset = 0.5f;
    public int attackDamage = 10;
    public float knockbackForce = 5f;
    public float attackRate = 2f;

    public LayerMask hitboxLayer;

    private float nextAttackTime = 0f;
    private bool isAttacking = false;

    void Start() => animator = GetComponent<Animator>();

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking && Time.time >= nextAttackTime)
        {
            float lastX = animator.GetFloat("LastInputX");
            float lastY = animator.GetFloat("LastInputY");

            if (lastX == 0f && lastY == 0f) lastY = -1f;

            Vector2 attackDirection = new Vector2(lastX, lastY).normalized;

            animator.SetBool("isAttacking", true);
            isAttacking = true;

            PerformHit(attackDirection);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void PerformHit(Vector2 attackDirection)
    {
        Vector2 worldAttackPoint = (Vector2)transform.position + (attackDirection * attackOffset);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(worldAttackPoint, attackRange, hitboxLayer);
        bool hitSomething = false;

        foreach (Collider2D hit in hitEnemies)
        {
            EnemyHealth enemyHealth = hit.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage, attackDirection, knockbackForce);
                hitSomething = true;
            }
        }

        SoundEffectManager.Play(hitSomething ? "SwordHit" : "SwordMiss", hitSomething);
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 drawPos = (Vector2)transform.position + (Vector2.down * attackOffset);
        Gizmos.DrawWireSphere(drawPos, attackRange);
    }
}