using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform currentEnemy;

    [SerializeField] protected float attackCooldown = 1;
    protected float lastTimeAttacked;

    [Header("Tower Settings")]
    [SerializeField] protected Transform towerHead;
    [SerializeField] protected float rotationSpeed = 10f;
    private bool canRotate;

    [SerializeField] protected float attackRange = 2.5f;
    [SerializeField] protected LayerMask enemyLayer;

    private Quaternion defaultRotation;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Update()
    {
        if(currentEnemy == null)
        { 
            currentEnemy = FindRandomEnemyWithinRange();
            return;
        }

        if (CanAttack())
            Attack();

        if (Vector3.Distance(currentEnemy.position, transform.position) > attackRange)
            currentEnemy = null;
        
        RotateTowardsEnemy();
    }

    protected virtual void Attack()
    {
        Debug.Log("Attacking " + currentEnemy.name);
    }

    protected bool CanAttack()
    {
        if (Time.time > lastTimeAttacked + attackCooldown)
        {
            lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

    protected Transform FindRandomEnemyWithinRange()
    {
        List<Transform> possibleTargets = new List<Transform>();
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in enemiesInRange)
        {
            possibleTargets.Add(enemy.transform);
        }
        
        int randomIndex = Random.Range(0, possibleTargets.Count);

        // If no enemies in range, return null
        if (possibleTargets.Count <= 0)
            return null;


        return possibleTargets[randomIndex];
    }

    public void EnableRotation(bool enable)
    {
        canRotate = enable;
    }

    protected virtual void RotateTowardsEnemy()
    {
        if(!canRotate)
            return;

        if (currentEnemy == null)
            return;

        Vector3 directionToEnemy = currentEnemy.position - towerHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        var rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        
        towerHead.rotation = Quaternion.Euler(rotation);
    }

    protected Vector3 DirectionToEnemyFrom(Transform startPoint)
    { 
        return (currentEnemy.position - startPoint.position).normalized;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
