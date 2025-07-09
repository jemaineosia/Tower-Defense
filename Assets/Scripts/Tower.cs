using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform currentEnemy;

    [Header("Tower Settings")]
    [SerializeField] private Transform towerHead;
    [SerializeField] private float rotationSpeed = 5f;
    //[SerializeField] private float attackRange = 10f;
    //[SerializeField] private float attackCooldown = 1f;
    //[SerializeField] private float damage = 10f;
    //private float attackTimer = 0f;
    //private Enemy currentEnemyScript;

    private void Update()
    {
        if (currentEnemy != null)
        {
            RotateTowardsEnemy();
            // HandleAttack();
        }
    }

    private void RotateTowardsEnemy()
    {
        Vector3 directionToEnemy = currentEnemy.position - towerHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        var rotation = Quaternion.Slerp(towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        
        towerHead.rotation = Quaternion.Euler(rotation);
    }
}
