using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private int health = 100;

    //Navmesh
    private NavMeshAgent agent;
    private Transform playerTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;

    //Patrolling
    private Vector3 walkPoint;
    private float walkPointRange = 5f;
    private bool walkPointSet;

    //Detecting
    private float sightRange = 10f, attackRange = 5f;
    private bool isInSightRange, isInAttackRange;

    //Attacking
    private float attackDelay = 1f;
    private bool isAttacking;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        isInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        isInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!isInSightRange && !isInAttackRange)
        {
            Patrolling();
        }
        else if (isInSightRange && !isInAttackRange)
        {
            DetectPlayer();
        }
        else if (isInSightRange && isInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        if (!isAttacking)
        {
            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 18f, ForceMode.Impulse);

            isAttacking = true;
            Invoke("ResetAttack", attackDelay);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    private void DetectPlayer()
    {
        agent.SetDestination(playerTransform.position);
        transform.LookAt(playerTransform);
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            float randomXPos = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomZPos = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x + randomXPos, transform.position.y, transform.position.z + randomZPos);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            {
                walkPointSet = true;
            }
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;

        CheckEnemyDied();
    }

    private void CheckEnemyDied()
    {
        if (health <= 0)
        {
            EnemyDied();
        }
    }

    private void EnemyDied()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
