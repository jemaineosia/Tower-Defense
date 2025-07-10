using UnityEngine;

public class TowerCrossbow : Tower
{
    private CrossbowVisuals crossbowVisuals;

    [Header("Crossbow Settings")]
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
        }
    }

    protected override void Awake()
    {
        base.Awake();

        crossbowVisuals = GetComponent<CrossbowVisuals>();
    }
}
