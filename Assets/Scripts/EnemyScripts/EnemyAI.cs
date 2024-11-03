using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public EnemyStats stats;
    private float maxHealth;
    private float currentHealth;
    private float speed;

    // Referenties naar speler en NavMeshAgent
    public Transform player;
    public NavMeshAgent agent;

    // Instellingen voor zicht- en aanvalsbereik
    public float sightRange = 5f;
    public float attackRange = 1.5f;
    public float roamRadius = 5f;
    public float attackCooldown = 2f;

    // Statussen
    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool isAttacking = false;
    bool weakened = false;

    // Tijdelijke doelpositie voor roaming
    private Vector3 roamTarget;

    // Referentie naar het gezondheidssysteem van de speler
    private PlayerStats playerStats;

    // Cooldown timer voor aanvallen
    private float lastAttackTime;

    // Animator voor het aansturen van animaties
    private Animator animator;

    private Slider healthBar;  // Sleep hier de health bar Slider naartoe

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();  // Haal de Animator component op
        playerStats = player.GetComponent<PlayerStats>();

        healthBar = GetComponentInChildren<Slider>();
        if (healthBar == null)
        {
            Debug.LogError("Health Bar Slider not found on " + gameObject.name);
        }


        maxHealth = stats.maxHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();

        speed = stats.speed;

        lastAttackTime = -attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
            return;
        }

        // Check afstanden tussen zombie en speler
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerInSightRange = distanceToPlayer <= sightRange;
        playerInAttackRange = distanceToPlayer <= attackRange;

        // Gedragskeuzes op basis van de afstand tot de speler
        if (playerInAttackRange)
        {
            AttackPlayer();  // Val de speler aan
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);

            ChasePlayer();  // Achtervolg de speler
        }
        else if ((!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && animator.GetBool("Idle") == false))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.Play("Idle");
        }

    }

    public void Roam()
    {
        agent.speed = 10;
        roamTarget = GetRandomRoamPosition();
        agent.SetDestination(roamTarget);
        animator.SetBool("Walk", true); 
        animator.SetBool("Idle", false);

    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);

        agent.speed *= 1.5f;

        // Achtervolg animatie
    }

    public void ToggleAttack(int value)
    {
        isAttacking = (value != 0);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }


    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        if (Time.time > lastAttackTime + attackCooldown)
        {
            transform.LookAt(player);
            animator.SetBool("Walk", false);
            animator.Play("Attack");
            lastAttackTime = Time.time;
        }
    }

    Vector3 GetRandomRoamPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
        return hit.position;
    }

    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth < maxHealth/2 && !weakened) 
        {
            weakened = true;
            animator.Play("TakeHit");   
        }
        Debug.Log(currentHealth);
    }

    void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;  // Zorg dat de Slider min = 0, max = 1
    }

    private void Die()
    {
        animator.Play("Death");
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        // Na een korte vertraging verdwijnt de zombie
        Destroy(gameObject, 1.1f);
    }
   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
