using System.Collections.Generic;
using UnityEngine;

public class TouchBoundary : MonoBehaviour
{
    public Camera mainCamera;
    private BoxCollider boxCollider;
    private List<BoxCollider> intersectingNotes = new List<BoxCollider>();

    private BattleManager battleManager;

    private int rawScore = 0;
    private bool isCountdownComplete = false;

    void Start()
    {
        // Cache the collider
        this.boxCollider = this.GetComponent<BoxCollider>();

        // Cache the battle manager
        this.battleManager = ModelLocator.GetModelInstance<BattleManager>() as BattleManager;
    }

    void Update()
    {
        // Don't register touch input before stage countdown has completed
        if (!this.isCountdownComplete)
        {
            // Ask battle manager if countdown is complete
            this.isCountdownComplete = this.battleManager.IsCountdownComplete;
            return;
        }

        // TODO: register user multi-touch input
        // If there is a touch input
        if (Input.touchCount > 0)
        {
            // Cast the touch to a ray
            Ray ray = this.mainCamera.ScreenPointToRay(Input.touches[0].position);

            // Debug visual
            //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue);

            // If the ray intersects the collider of the TouchBoundary
            if (this.boxCollider.Raycast(ray, out RaycastHit hit, 100f))
            {
                // Check if the ray is also intersecting any Notes that are
                // currently colliding with the TouchBoundary
                foreach (BoxCollider collider in this.intersectingNotes)
                {
                    // If the ray intersects the collider of such a Note
                    if (collider.Raycast(ray, out RaycastHit collHit, 100f))
                    {
                        // Increase the raw score while the touch is held and
                        // the ray intersects both the TouchBoundary and Note
                        this.rawScore++;
                        this.battleManager.RawScore = this.rawScore;
                    }
                }
            }
        }

    }

    // Add colliding Note to cache
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Note>())
        {
            // A Note is entering the collider of the Boundary, add to list
            this.intersectingNotes.Add(other.gameObject.GetComponent<BoxCollider>());
        }
    }

    // Remove exiting Note from cache
    private void OnTriggerExit(Collider other)
    {
        // A Note is exiting the collider of the Boundary, remove from list
        this.intersectingNotes.Remove(other.gameObject.GetComponent<BoxCollider>());
    }

}
