using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy currentEnemy;

    [SerializeField] protected float attackCooldown = 1;
    protected float lastTimeAttacked;

    [Header("Tower Settings")]
    [SerializeField] protected EnemyType enemyPriorityType = EnemyType.None;
    [SerializeField] protected Transform towerHead;
    [SerializeField] protected float rotationSpeed = 10f;
    private bool canRotate;

    [SerializeField] protected float attackRange = 2.5f;
    [SerializeField] protected LayerMask enemyLayer;

    [Space]
    [Tooltip("Enabling this allows tower to change target between attacks")]
    [SerializeField] private bool dynamicTargetChange = true;
    private float targetCheckInterval = 0.1f;
    private float lastTimeCheckedTarget;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Update()
    {
        UpdateTargetIfNeeded();

        if (currentEnemy == null)
        { 
            currentEnemy = FindEnemyWithinRange();
            return;
        }

        if (CanAttack())
            Attack();

        if (Vector3.Distance(currentEnemy.CenterPoint(), transform.position) > attackRange)
            currentEnemy = null;
        
        RotateTowardsEnemy();
    }

    private void UpdateTargetIfNeeded()
    {
        if (!dynamicTargetChange)
            return;

        if(Time.time > lastTimeCheckedTarget + targetCheckInterval)
        {
            lastTimeCheckedTarget = Time.time;
            currentEnemy = FindEnemyWithinRange();
        }
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

    protected Enemy FindEnemyWithinRange()
    {
        List<Enemy> priorityTargets = new List<Enemy>();
        List<Enemy> possibleTargets = new List<Enemy>();
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in enemiesInRange)
        {
            Enemy newEnemy = enemy.GetComponent<Enemy>();
            EnemyType enemyType = newEnemy.GetEnemyType();

            if (enemyType == enemyPriorityType)
                priorityTargets.Add(newEnemy);
            else
                possibleTargets.Add(newEnemy);
        }
      
        if (priorityTargets.Count > 0)
            return GetMostAdvancedEnemy(priorityTargets);

        if(possibleTargets.Count > 0)
            return GetMostAdvancedEnemy(possibleTargets);

        return null;
    }

    private static Enemy GetMostAdvancedEnemy(List<Enemy> possibleTargets)
    {
        Enemy mostAdvancedEnemy = null;
        float minRemaintingDistance = float.MaxValue;

        foreach (Enemy enemy in possibleTargets)
        {
            if (enemy == null)
                continue;
            float remainingDistance = enemy.DistanceToFinishLine();
            if (remainingDistance < minRemaintingDistance)
            {
                minRemaintingDistance = remainingDistance;
                mostAdvancedEnemy = enemy;
            }
        }

        return mostAdvancedEnemy;
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

        Vector3 directionToEnemy = DirectionToEnemyFrom(towerHead);
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        var rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        
        towerHead.rotation = Quaternion.Euler(rotation);
    }

    protected Vector3 DirectionToEnemyFrom(Transform startPoint)
    { 
        return (currentEnemy.CenterPoint() - startPoint.position).normalized;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
