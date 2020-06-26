using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdventureMenu : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public GameObject RootPosition;
    public GameObject CardSelectOverlay;
    public GameObject SelectedCard;
    public GameObject StageDescription;
    public GameObject CardList;

    private UserManager userManager;
    private StoryManager storyManager;

    private int currentLevel;
    private int currentStage;

    private void Start()
    {
        // Cache the user and stage managers
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.storyManager = ModelLocator.GetModelInstance<StoryManager>() as StoryManager;

        // Cache current level and stage
        this.currentLevel = this.userManager.GetUserData().GetCurrentLevel();
        this.currentStage = this.userManager.GetUserData().GetCurrentStage();

        // Layout UI
        this.SetTitle();
        this.ShowCardSelectOverlay(false);
        this.LayoutMenuWithStageButtons();
    }

    // Load stage data from database
    public void SelectStage(int stage)
    {
        // called onClick of a Stage Button
        this.LoadStage(stage);
    }

    // Show Card List
    public void ShowCardList()
    {
        this.CardList.SetActive(true);
        this.CardList.GetComponent<CardList>().LayoutAvailableCards();
    }

    // Confirm card selection for stage
    public void ConfirmCardSelection()
    {
        // Add selected card data to user data
        CardData cardData = this.CardList.GetComponent<CardList>().GetSelectedCardData();
        this.userManager.GetUserData().SetSelectedCardData(cardData);

        // Dismiss card list
        this.CardList.SetActive(false);

        // Show selected card data detail in this.SelectedCard
        this.SelectedCard.GetComponent<SelectedCard>().ShowSelectedCardDetail(cardData);
    }

    public void StartStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Set stage data in stage manager
    private void LoadStage(int stage)
    {
        StageData stageData = DataInitializer.GetStageData(this.currentLevel, stage);
        MetaData metaData = this.userManager.GetUserData().GetMetaDataById(this.currentLevel, stage);
        this.storyManager.SetStageData(stageData);
        this.storyManager.SetMetaData(metaData);
        this.ShowCardSelectOverlay(true);
    }

    // Set text of title above Stage Buttons
    private void SetTitle()
    {
        string text = string.Format("Level {0}", this.currentLevel);
        this.Title.text = text;
    }

    private void ShowCardSelectOverlay(bool show)
    {
        this.CardSelectOverlay.SetActive(show);
        this.SelectedCard.SetActive(show);
        this.StageDescription.SetActive(show);
        this.CardList.SetActive(false);
    }

    // Populate the Adventure menu with Stage buttons using story data
    private void LayoutMenuWithStageButtons()
    {
        List<StageData> stages = DataInitializer.GetStagesOfLevel(this.currentLevel);

        // Create a Stage Button for each stage in this level
        float spacing = 150f;

        for (int i = 0; i < stages.Count; i++)
        {
            // Create the new Stage Button
            GameObject stageObject = new GameObject();
            Button stageButton = stageObject.AddComponent<Button>();

            int j = i; // Delegate argument for onClick listener StartStage()
            stageButton.onClick.AddListener(delegate { this.SelectStage(j); });
            stageButton.interactable = i <= this.currentStage;

            // Set button label text
            TextMeshProUGUI stageButtonText = stageButton.gameObject.AddComponent<TextMeshProUGUI>();
            stageButtonText.text = DataInitializer.FormatStageId(stages[i].level, stages[i].stage);
            stageButtonText.alignment = TextAlignmentOptions.Center;

            // Position buttons equidistant for now - think of something fun later TODO
            RectTransform stageObjectRect = stageObject.GetComponent<RectTransform>();
            stageObject.transform.SetParent(this.RootPosition.transform);
            stageObjectRect.anchorMin = new Vector2(0, 0.5f);
            stageObjectRect.anchorMax = new Vector2(0, 0.5f);
            stageObjectRect.pivot = new Vector2(0, 0.5f);
            stageObject.transform.localPosition = new Vector3(i * spacing, 0);
        }
    }

}
