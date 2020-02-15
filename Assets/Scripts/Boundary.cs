using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Camera mainCamera;
    private BoxCollider boxCollider;
    private List<BoxCollider> intersectingNotes = new List<BoxCollider>();
    private int scoreValue = 0;

    private StageManager stageManager;

    void Start()
    {
        // Cache the collider
        this.boxCollider = this.GetComponent<BoxCollider>();

        // Cache the stage manager
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
    }

    void Update()
    {
        // TODO: register user multi-touch input
        if (Input.touchCount > 0)
        {
            Ray ray = this.mainCamera.ScreenPointToRay(Input.touches[0].position);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
            if (this.boxCollider.Raycast(ray, out RaycastHit hit, 100f))
            {
                // The touch intersects the collider on the Boundary
                // Now check if the touch intersects any Notes that are currently colliding with Boundary
                foreach (BoxCollider collider in this.intersectingNotes)
                {
                    if (collider.Raycast(ray, out RaycastHit collHit, 100f))
                    {
                        // The touch intersects a Note that is currently colliding with the Boundary
                        // Increase score while touch is held down and intersects both Boundary and Note
                        this.scoreValue++;
                        this.stageManager.SetScore(this.scoreValue);
                    }
                }
            }
        }

    }

    // Add colliding Notes to cache
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
