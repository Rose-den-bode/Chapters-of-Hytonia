using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public MeleeAttack meleeAttack;  // Referentie naar MeleeAttack script
    public Weapons weapon;
    public EnemyAI enemyAi;
    private List<Collider> hitEnemies = new List<Collider>(); // Lijst van vijanden die tijdens deze slag geraakt zijn


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && meleeAttack.IsAttacking() && !hitEnemies.Contains(other) && weapon != null)
        {
            hitEnemies.Add(other);
            other.GetComponent<EnemyAI>().ToggleAttack(0);
            other.GetComponent<EnemyAI>().DealDamage(weapon.Damage + PlayerStats.Instance.damageModifier * weapon.Damage);
        }
        if (other.CompareTag("Player") && enemyAi != null && enemyAi.IsAttacking())
        {
            other.GetComponent<PlayerStats>().TakeDamage(enemyAi.stats.damage);
        }
    }

    public void ResetHitEnemies()
    {
        hitEnemies.Clear();
    }
}
