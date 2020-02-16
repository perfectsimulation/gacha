using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Camera mainCamera;
    private BoxCollider boxCollider;
    private List<BoxCollider> intersectingNotes = new List<BoxCollider>();

    private float scoreMultiplier = 1f;
    private float scoreValue = 0f;

    private StageManager stageManager;
    private UserManager userManager;

    private StageData stageData;
    private CardData selectedCard;

    void Start()
    {
        // Cache the collider
        this.boxCollider = this.GetComponent<BoxCollider>();

        // Cache the user and stage managers
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;

        this.stageData = this.stageManager.GetStageData();
        this.selectedCard = this.userManager.GetUserData().GetSelectedCardData();
        this.scoreMultiplier = this.CalculateScoreMultiplier(this.selectedCard);
    }

    void Update()
    {
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
                        this.stageManager.SetScore(this.scoreValue * this.scoreMultiplier);
                    }
                }
            }
        }

    }

    // Calculate score multiplier bonus from selected card
    private float CalculateScoreMultiplier(CardData cardData)
    {
        CardBonus cardBonus = this.stageData.cardBonus;
        float redBonus = cardData.red * (1f + cardBonus.redX);
        float orangeBonus = cardData.orange * (1f + cardBonus.orangeX);
        float yellowBonus = cardData.yellow * (1f + cardBonus.yellowX);
        float greenBonus = cardData.green * (1f + cardBonus.greenX);
        float blueBonus = cardData.blue * (1f + cardBonus.blueX);
        float purpleBonus = cardData.purple * (1f + cardBonus.purpleX);
        float pinkBonus = cardData.pink * (1f + cardBonus.pinkX);
        float totalBonus = redBonus + orangeBonus + yellowBonus + greenBonus + blueBonus + purpleBonus + pinkBonus;
        return totalBonus / 7f;
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
