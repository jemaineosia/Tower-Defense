using UnityEngine;

public class TowerCrossbow : Tower
{
    private CrossbowVisuals crossbowVisuals;

    [Header("Crossbow Settings")]
    [SerializeField] private int damage;
    [SerializeField] private Transform gunPoint;

    override protected void Attack()
    {

        Vector3 directionToEnemy = DirectionToEnemyFrom(gunPoint);

        if (Physics.Raycast(gunPoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity))
        {
            towerHead.forward = directionToEnemy;

            Debug.DrawLine(gunPoint.position, hitInfo.point, Color.red);

            crossbowVisuals.PlayAttackVFX(gunPoint.position, hitInfo.point);
            crossbowVisuals.PlayReloadVFX(attackCooldown);

            IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Hit object does not implement IDamageable: " + hitInfo.collider.name);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        crossbowVisuals = GetComponent<CrossbowVisuals>();
    }
}
