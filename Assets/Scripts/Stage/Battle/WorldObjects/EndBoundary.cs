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
        // If the TouchBoundary has reached this EndBoundary
        if (other.gameObject.GetComponent<TouchBoundary>())
        {
            // Tell the battle manager to end the stage
            this.battleManager.EndStage();
        }
    }
}
