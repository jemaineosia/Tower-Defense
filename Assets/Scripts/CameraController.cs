using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 120f;

    private float smoothTime = 0.1f;
    private Vector3 movementVelocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 targetPosition = transform.position;

        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        if(hInput > 0)
            targetPosition += transform.right * movementSpeed * Time.deltaTime;
        
        if(hInput < 0)
            targetPosition -= transform.right * movementSpeed * Time.deltaTime;

        if(vInput > 0)
            targetPosition += transform.forward * movementSpeed * Time.deltaTime;

        if(vInput < 0)
            targetPosition -= transform.forward * movementSpeed * Time.deltaTime;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref movementVelocity, smoothTime);
    }
}
