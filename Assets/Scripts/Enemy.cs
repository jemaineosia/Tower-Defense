using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Basic,
    Fast,
    None
}

public class Enemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;

    [SerializeField] private EnemyType enemyType = EnemyType.None;
    [SerializeField] private Transform centerPoint;

    public int healthPoints = 4;

    [Header("Movement")]
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private Transform[] waypoints;
    private int waypointIndex = 0;
    
    private float totalDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Start()
    {
        waypoints = FindFirstObjectByType<WaypointManager>().GetWaypoints;
        CollectTotalDistance();
    }


    private void Update()
    {
        FaceTarget(agent.steeringTarget);
        // Check if we've reached the waypoint (within a small distance)
        if (agent.remainingDistance < 0.5f)
        {
            if (waypointIndex < waypoints.Length)
            {
                agent.SetDestination(GetNextWaypoint());
            }
        }
    }

    public float DistanceToFinishLine() => totalDistance + agent.remainingDistance;
    private void CollectTotalDistance()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            totalDistance += Vector3.Distance(waypoints[i].position, waypoints[(i + 1) % waypoints.Length].position);
        }
    }
    private void FaceTarget(Vector3 newTarget)
    { 
        Vector3 directionToTarget = newTarget - transform.position;
        directionToTarget.y = 0; // Keep the direction on the horizontal plane

        if (directionToTarget.magnitude == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }
    private Vector3 GetNextWaypoint()
    {
        Vector3 nextWaypoint = waypoints[waypointIndex].position;

        if(waypointIndex > 0)
        {
            float distance = Vector3.Distance(waypoints[waypointIndex].position, waypoints[waypointIndex-1].position);
            totalDistance -= distance;
        }

        waypointIndex++;

        return nextWaypoint;
    }
    public Vector3 CenterPoint() => centerPoint.position;
    public EnemyType GetEnemyType() => enemyType;
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
            Destroy(gameObject);
    }
}
