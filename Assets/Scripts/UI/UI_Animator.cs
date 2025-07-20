using System.Collections;
using UnityEngine;

public class UI_Animator : MonoBehaviour
{
    public void ChangePosition(Transform transform, Vector3 offset, float duration = 0.1f)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        StartCoroutine(ChangePosition(rectTransform, offset, duration));
    }

    private IEnumerator ChangePosition(RectTransform rectTransform, Vector3 offset, float duration)
    {
        float time = 0;

        Vector3 initialPosition = rectTransform.anchoredPosition;
        Vector3 targetPosition = initialPosition + offset;

        while (time < duration)
        {
            float t = time / duration;
            rectTransform.anchoredPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            
            time += Time.deltaTime;
            
            yield return null;
        }
    }
}
