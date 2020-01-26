using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float mainCameraSpeed;
    private Transform mainCameraTransform;
    private Vector3 mainCameraForwardVector;

    void Start()
    {
        // Cache the main camera transform
        this.mainCameraTransform = this.mainCamera.transform;
        // Rotation for main camera forward vector
        Quaternion rotation = Quaternion.Euler(-10f, 0, 0);
        // Multiply main camera forward vector by main camera speed
        Vector3 velocity = this.mainCameraSpeed * Vector3.forward;
        // Cache the main camera forward vector
        this.mainCameraForwardVector = rotation * velocity;
    }

    void Update()
    {
        // Move main camera forward
        this.mainCameraTransform.Translate(mainCameraForwardVector);
    }
}
