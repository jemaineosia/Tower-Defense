using System.Collections;
using UnityEngine;

public class TileAnimator : MonoBehaviour
{
    [SerializeField] private float yMovementDuration = .1f;

    [Header("Build Slot Movement")]
    [SerializeField] private float buildSlotYOffset = .25f;

    public void MoveTile(Transform objectToMove, Vector3 targetPosition)
    {
        if (objectToMove == null)
        {
            Debug.LogError("Object to move is null");
            return;
        }
        if (targetPosition == null)
        {
            Debug.LogError("Target position is null");
            return;
        }
        StartCoroutine(MoveTileCo(objectToMove, targetPosition));
    }

    public IEnumerator MoveTileCo(Transform objectToMove, Vector3 targetPosition)
    {
        float time = 0;
        Vector3 startPosition = objectToMove.position;
        while (time < yMovementDuration)
        {
            float t = time / yMovementDuration;
            objectToMove.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        objectToMove.position = targetPosition;
    }

    public float GetBuildOffset() => buildSlotYOffset;
    public float GetTravelDuration() => yMovementDuration;
}
