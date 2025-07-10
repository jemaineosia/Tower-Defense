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

    [Header("Rotor Visuals")]
    [SerializeField] private Transform rotor;
    [SerializeField] private Transform rotorUnloaded;
    [SerializeField] private Transform rotorLoaded;

    [Header("Front Glow String")]
    [SerializeField] private LineRenderer frontString_L;
    [SerializeField] private LineRenderer frontString_R;

    [Space]
    [SerializeField] private Transform frontStartPoint_L;
    [SerializeField] private Transform frontStartPoint_R;
    [SerializeField] private Transform frontEndPoint_L;
    [SerializeField] private Transform frontEndPoint_R;

    [Header("Black Glow String")]
    [SerializeField] private LineRenderer backString_L;
    [SerializeField] private LineRenderer backString_R;

    [Space]
    [SerializeField] private Transform backStartPoint_L;
    [SerializeField] private Transform backStartPoint_R;
    [SerializeField] private Transform backEndPoint_L;
    [SerializeField] private Transform backEndPoint_R;

    [SerializeField] private LineRenderer[] lineRenderers;

    private void Awake()
    {
        towerCrossbow = GetComponent<TowerCrossbow>();
        if (attackVisuals == null)
        {
            Debug.LogError("Attack visuals LineRenderer is not assigned in " + gameObject.name);
        }

        material = new Material(meshRenderer.material);
        meshRenderer.material = material;
        UpdateMaterialsOnLineRenderers();
        StartCoroutine(ChangeEmission(1));

    }

    private void UpdateMaterialsOnLineRenderers()
    {
        foreach (LineRenderer lr in lineRenderers)
        {
            if (lr == null)
            {
                Debug.LogError("A LineRenderer is not assigned in " + gameObject.name);
            }

            lr.material = material;
        }
    }

    private void Update()
    {
        UpdateEmissionColor();
        UpdateStrings();
    }

    private void UpdateStrings()
    {
        UpdateStringVisual(frontString_R, frontStartPoint_L, frontEndPoint_L);
        UpdateStringVisual(frontString_L, frontStartPoint_R, frontEndPoint_R);
        UpdateStringVisual(backString_R, backStartPoint_L, backEndPoint_L);
        UpdateStringVisual(backString_L, backStartPoint_R, backEndPoint_R);
    }

    private void UpdateEmissionColor()
    {
        Color emissionColor = Color.Lerp(startColor, endColor, currentIntensity / maxIntensity);
        material.SetColor("_EmissionColor", emissionColor * currentIntensity);
    }

    public void PlayReloadVFX(float duration)
    {
        float newDuration = duration / 2;

        StartCoroutine(ChangeEmission(newDuration));
        StartCoroutine(UpdateRotorPosition(newDuration));
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

    private IEnumerator UpdateRotorPosition(float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            rotor.position = Vector3.Lerp(rotorUnloaded.position, rotorLoaded.position, t);
            yield return null;
        }

        rotor.position = rotorLoaded.position;
    }

    private void UpdateStringVisual(LineRenderer lineRenderer, Transform startPoint, Transform endPoint)
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
    }
}
