using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f; // Range of the sword attack
    public int attackDamage = 1; // Damage dealt per attack
    public LayerMask enemyLayer; // Layer to identify enemies
    public Transform attackPoint; // Point from where the attack originates
    public float attackCooldown = 1f; // Time between attacks

    private bool canAttack = true;

    private void Update()
    {
        // Check for attack input
        if (Input.GetButtonDown("Fire1") && canAttack) // "Fire1" is usually mapped to the left mouse button
        {
            Attack();
        }
    }

    private void Attack()
    {
        canAttack = false;

        // Detect enemies in range of the attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        // Damage each enemy
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.TakeHit();
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }

        // Reset attack cooldown
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the editor
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
