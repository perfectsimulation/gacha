using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float mainCameraSpeed;

    private StageManager stageManager;

    private Transform mainCameraTransform;
    private Vector3 mainCameraForwardVector;
    private bool isCountdownComplete = false;
    private bool isStageOver = false;

    void Start()
    {
        // Cache the stage manager
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
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
        // Only move camera forward once stage start countdown is complete
        if (!this.isCountdownComplete)
        {
            // Ask stage manager if countdown is complete
            this.isCountdownComplete = stageManager.IsCountdownComplete();
            return;
        }

        // Move main camera forward while the stage is not over
        if (!this.isStageOver)
        {
            this.mainCameraTransform.Translate(mainCameraForwardVector);
            this.isStageOver = stageManager.IsStageOver();
            return;
        }

    }

}
