using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;

    public int healthPoints = 4;

    [Header("Movement")]
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private Transform[] waypoints;
    private int waypointIndex = 0;

    [Space]
    public float totalDistance;
    public float remainingDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Start()
    {
        waypoints = FindFirstObjectByType<WaypointManager>().GetWaypoints;
        for(int i = 0; i < waypoints.Length; i++)
        {
            totalDistance += Vector3.Distance(waypoints[i].position, waypoints[(i + 1) % waypoints.Length].position);
        }
    }

    private void Update()
    {
        remainingDistance = totalDistance - agent.remainingDistance;
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
        waypointIndex++;

        return nextWaypoint;
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
            Destroy(gameObject);
    }
}
