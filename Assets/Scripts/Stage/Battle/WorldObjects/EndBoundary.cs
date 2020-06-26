using UnityEngine;

public class EndBoundary : MonoBehaviour
{
    private BattleManager battleManager;

    void Start()
    {
        // Cache the stage manager
        this.battleManager = ModelLocator.GetModelInstance<BattleManager>() as BattleManager;
    }

    // Watch for stage completion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TouchBoundary>())
        {
            // The touch boundary has reached this end boundary, so tell stage manager the stage is over
            this.battleManager.SetStageOver();
        }
    }
}
