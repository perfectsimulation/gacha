using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBoundary : MonoBehaviour
{
    private StageManager stageManager;

    void Start()
    {
        // Cache the stage manager
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
    }

    // Watch for stage completion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TouchBoundary>())
        {
            // The touch boundary has reached this end boundary, so tell stage manager the stage is over
            this.stageManager.SetStageOver();
        }
    }
}
