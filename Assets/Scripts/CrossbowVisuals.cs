using System.Collections;
using UnityEngine;

public class CrossbowVisuals : MonoBehaviour
{
    private TowerCrossbow towerCrossbow;

    [SerializeField] private LineRenderer attackVisuals;
    [SerializeField] private float attackVisualDuration = 0.1f;

    [Header("Glowing Visuals")]
    [SerializeField] private MeshRenderer meshRenderer;
    private Material material;
    [Space]
    private float currentIntensity;
    [SerializeField] private float maxIntensity = 150;
    [Space]
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private void Awake()
    {
        towerCrossbow = GetComponent<TowerCrossbow>();
        if (attackVisuals == null)
        {
            Debug.LogError("Attack visuals LineRenderer is not assigned in " + gameObject.name);
        }

        material = new Material(meshRenderer.material);
        meshRenderer.material = material;

        StartCoroutine(ChangeEmission(1));

    }

    private void Update()
    {
        UpdateEmissionColor();
    }

    private void UpdateEmissionColor()
    {
        Color emissionColor = Color.Lerp(startColor, endColor, currentIntensity / maxIntensity);
        material.SetColor("_EmissionColor", emissionColor * currentIntensity);
    }

    public void PlayReloadVFX(float duration)
    {
        StartCoroutine(ChangeEmission(duration / 2));
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

    private IEnumerator ChangeEmission(float duration)
    {
        float startTime = Time.time;
        float startIntensity = 0;

        while (Time.time - startTime < duration)
        {
            // calculate the intensity based on the elapsed time
            float t = (Time.time - startTime) / duration;
            currentIntensity = Mathf.Lerp(startIntensity, maxIntensity, t);
            yield return null;
        }

        currentIntensity = maxIntensity;
    }
}
