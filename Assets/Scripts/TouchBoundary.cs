using System.Collections.Generic;
using UnityEngine;

public class TouchBoundary : MonoBehaviour
{
    public Camera mainCamera;
    private BoxCollider boxCollider;
    private List<BoxCollider> intersectingNotes = new List<BoxCollider>();

    private StoryManager storyManager;
    private UserManager userManager;

    private float scoreMultiplier = 1f;
    private float scoreValue = 0f;
    private bool isCountdownComplete = false;

    private StageData stageData;
    private CardData selectedCard;

    void Start()
    {
        // Cache the collider
        this.boxCollider = this.GetComponent<BoxCollider>();

        // Cache the user and stage managers
        this.storyManager = ModelLocator.GetModelInstance<StoryManager>() as StoryManager;
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;

        this.stageData = this.storyManager.GetStageData();
        this.selectedCard = this.userManager.GetUserData().GetSelectedCardData();
        this.scoreMultiplier = this.CalculateScoreMultiplier(this.selectedCard);
    }

    void Update()
    {
        // Don't register touch input before stage countdown has completed
        if (!this.isCountdownComplete)
        {
            // Ask stage manager if countdown is complete
            this.isCountdownComplete = storyManager.IsCountdownComplete();
            return;
        }

        // TODO: register user multi-touch input
        if (Input.touchCount > 0)
        {
            Ray ray = this.mainCamera.ScreenPointToRay(Input.touches[0].position);
            //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue);
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
                        this.storyManager.SetScore(this.scoreValue * this.scoreMultiplier);
                    }
                }
            }
        }

    }

    // Calculate score multiplier bonus from selected card
    private float CalculateScoreMultiplier(CardData cardData)
    {
        // No score multiplier if no card was selected
        if (cardData == null)
        {
            return 1f;
        }

        CardBonus cardBonus = this.stageData.cardBonus;
        float waterBonus = cardData.waterAffinity * (1f + cardBonus.waterX);
        float airBonus = cardData.airAffinity * (1f + cardBonus.airX);
        float hotBonus = cardData.hotAffinity * (1f + cardBonus.hotX);
        float coldBonus = cardData.coldAffinity * (1f + cardBonus.coldX);
        float totalBonus = waterBonus + airBonus + hotBonus + coldBonus;
        return totalBonus / 4f;
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
