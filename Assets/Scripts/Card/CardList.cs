using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created when the user selects a stage of a level in the AdventureMenu
public class CardList : MonoBehaviour
{
    public GameObject CardPreviewPrefab;
    public Transform FirstCardPreviewPosition;
    public Button ConfirmButton;

    // Updated when the user chooses a card from the card list just before
    // starting the stage
    public Card SelectedCard
    {
        get { return this.selectedCard; }
        set { this.selectedCard = value; }
    }

    private UserManager userManager;

    private List<Card> cards;
    private Card selectedCard;

    void Start()
    {
        // Cache the user manager
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.cards = this.userManager.GetUserData().GetUserCards();
        this.ConfirmButton.interactable = false;
    }

    // Display all cards the user can select
    public void LayoutAvailableCards()
    {
        // Create a card preview for each card
        float spacing = 140f;
        // Instantiate a CardPreview for each card
        for (int i = 0; i < this.cards.Count; i++)
        {
            // Create the new CardPreview
            GameObject cardPreview = Instantiate(this.CardPreviewPrefab);
            Button cardPreviewButton = cardPreview.GetComponentInChildren<Button>();

            int j = i; // Delegate argument for onClick listener SelectCard()
            cardPreviewButton.onClick.AddListener(delegate { this.SelectCard(j); });

            // Set button label text
            cardPreviewButton.GetComponentInChildren<Text>().text = this.cards[i].name;

            // Position buttons equidistant for now - think of something fun later TODO
            cardPreview.transform.SetParent(this.FirstCardPreviewPosition);
            cardPreview.transform.localPosition = new Vector3(i * spacing, 0);

            // Add Card to CardPreview component
            cardPreview.GetComponent<CardPreview>().SetCard(this.cards[i]);
        }
    }

    private void SelectCard(int i)
    {
        this.ConfirmButton.interactable = true;
        this.SelectedCard = this.cards[i];
    }
}
