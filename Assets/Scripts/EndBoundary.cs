using UnityEngine;

public class EndBoundary : MonoBehaviour
{
    private StoryManager storyManager;

    void Start()
    {
        // Cache the stage manager
        this.storyManager = ModelLocator.GetModelInstance<StoryManager>() as StoryManager;
    }

    // Watch for stage completion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TouchBoundary>())
        {
            // The touch boundary has reached this end boundary, so tell stage manager the stage is over
            this.storyManager.SetStageOver();
        }
    }
}
