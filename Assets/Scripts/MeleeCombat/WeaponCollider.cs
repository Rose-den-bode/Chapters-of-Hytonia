using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public MeleeAttack meleeAttack;  // Referentie naar MeleeAttack script
    public Weapons weapon;
    public EnemyStats enemyStats;
    private List<Collider> hitEnemies = new List<Collider>(); // Lijst van vijanden die tijdens deze slag geraakt zijn


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && meleeAttack.IsAttacking() && !hitEnemies.Contains(other) && weapon != null)
        {
            hitEnemies.Add(other);
            other.GetComponent<EnemyAI>().DealDamage(weapon.Damage);
        }
        if (other.CompareTag("Player") && meleeAttack.IsAttacking() && !hitEnemies.Contains(other) && enemyStats != null)
        {
            hitEnemies.Add(other);
            other.GetComponent<EnemyAI>().DealDamage(enemyStats.damage);
        }
    }

    public void ResetHitEnemies()
    {
        hitEnemies.Clear();
    }
}
