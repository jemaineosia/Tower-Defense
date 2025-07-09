using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform currentEnemy;

    [Header("Tower Settings")]
    [SerializeField] private Transform towerHead;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float attackRange = 1.5f;
    //[SerializeField] private float attackCooldown = 1f;
    //[SerializeField] private float damage = 10f;
    //private float attackTimer = 0f;
    //private Enemy currentEnemyScript;

    private Quaternion defaultRotation;

    private void Start()
    {
        defaultRotation = towerHead.rotation;
    }

    private void Update()
    {
        if (currentEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, currentEnemy.position);
            if (distanceToEnemy <= attackRange)
            {
                RotateTowardsEnemy();
            }
            else
            {
                ReturnToDefaultRotation();
            }
        }
        else
        {
            ReturnToDefaultRotation();
        }
    }

    private void ReturnToDefaultRotation()
    {
        var rotation = Quaternion.Slerp(towerHead.rotation, defaultRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        towerHead.rotation = Quaternion.Euler(rotation);
    }

    private void RotateTowardsEnemy()
    {
        Vector3 directionToEnemy = currentEnemy.position - towerHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        var rotation = Quaternion.Slerp(towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        
        towerHead.rotation = Quaternion.Euler(rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
