using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private Animator animator;
    private bool attacking;
    public new Rigidbody rigidbody;
    float currentYRotation;
    public WeaponCollider weaponColliderScript;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (attacking)
        {
            transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        }

        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            Attack();
        }
    }

    public void ChangeAttackBool(int value)
    {
        attacking = (value != 0);
    }

    public void Attack()
    {
        currentYRotation = transform.rotation.eulerAngles.y;
        animator.Play("MeeleeAttack_OneHanded");
    }

    // Voeg een getter toe voor de attacking-status
    public bool IsAttacking()
    {
        return attacking;
    }

    public void ResetHitEnemies()
    {
        weaponColliderScript.ResetHitEnemies();
    }
}
