using System.Collections;
using UnityEngine;

public class CrossbowVisuals : MonoBehaviour
{
    private TowerCrossbow towerCrossbow;

    [SerializeField] private LineRenderer attackVisuals;
    [SerializeField] private float attackVisualDuration = 0.1f;

    private void Awake()
    {
        towerCrossbow = GetComponent<TowerCrossbow>();
        if (attackVisuals == null)
        {
            Debug.LogError("Attack visuals LineRenderer is not assigned in " + gameObject.name);
        }
    }

    public void PlayAttackVFX(Vector3 startPosition, Vector3 endPosition)
    {
        StartCoroutine(VFXCoroutine(startPosition,endPosition));
    }

    private IEnumerator VFXCoroutine(Vector3 startPosition, Vector3 endPosition)
    {
        towerCrossbow.EnableRotation(false);

        attackVisuals.enabled = true;
        attackVisuals.SetPosition(0, startPosition);
        attackVisuals.SetPosition(1, endPosition);

        yield return new WaitForSeconds(attackVisualDuration);
        attackVisuals.enabled = false;
        towerCrossbow.EnableRotation(true);
    }
}
