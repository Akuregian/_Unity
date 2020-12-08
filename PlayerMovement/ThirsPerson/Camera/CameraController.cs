using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform TargetToFollow;
    public Vector3 offsetFromTarget;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float Pitch = 2f;

    public float yawSpeed = 100f;
    public float yawInput = 0f;

    private float currentZoom = 10f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);


        if(Input.GetKey("w"))
        {
            Debug.Log("CurrentZoom: " + currentZoom);
            currentZoom -= 1f;
        }

        yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = TargetToFollow.position - offsetFromTarget * currentZoom;
        transform.LookAt(TargetToFollow.position + Vector3.up * Pitch);

        transform.RotateAround(TargetToFollow.position, Vector3.up, yawInput);
    }
}
