using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject RootPosition;

    private UserManager userManager;
    private NetworkManager networkManager;

    private int currentLevel;
    private int currentStage;

    private void Start()
    {
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.networkManager = ModelLocator.GetModelInstance<NetworkManager>() as NetworkManager;

        this.currentLevel = this.userManager.GetUserData().GetCurrentLevel();
        this.currentStage = this.userManager.GetUserData().GetCurrentStage();

        this.SetTitle();
        this.LayoutMenuWithStageButtons();

        // TODO: stage creation script
        //NoteData[] notes00 = new NoteData[] {
        //    new NoteData(0, 0, 8),
        //    new NoteData(2, 8, 5),
        //    new NoteData(4, 13, 3),
        //    new NoteData(2, 16, 8),
        //    new NoteData(0, 24, 5),
        //    new NoteData(-2, 29, 3),
        //};
        //NoteData[] notes01 = new NoteData[] {
        //    new NoteData(0, 0, 4),
        //    new NoteData(-2, 4, 4),
        //    new NoteData(-4, 8, 2),
        //    new NoteData(0, 10, 4),
        //    new NoteData(4, 14, 2),
        //    new NoteData(2, 16, 2),
        //};
        //int[] scoreTier = new int[] { 100, 200, 300 };
        //StartCoroutine(this.networkManager.SaveStage(new StageData(0, 0, scoreTier, notes00)));
        //StartCoroutine(this.networkManager.SaveStage(new StageData(0, 1, scoreTier, notes01)));
    }

    // Load stage data from database
    public void StartStage(int stage)
    {
        // TODO: add argument for stage details
        // called onClick of a Stage Button
        Debug.Log(stage);
        StartCoroutine(LoadStage(stage));
    }

    // Ask the network manager for stage data for the current level and the selected stage
    private IEnumerator LoadStage(int stage)
    {
        yield return StartCoroutine(this.networkManager.LoadStage(this.currentLevel, stage));
    }

    // Set text of title above Stage Buttons
    private void SetTitle()
    {
        string text = string.Format("Level {0}", this.currentLevel);
        this.title.text = text;
    }

    // Populate the Adventure menu with Stage buttons using user data
    private void LayoutMenuWithStageButtons()
    {
        List<Node> stages = this.userManager.GetUserData().GetStagesOfLevel(this.currentLevel);

        // Create a Stage Button for each stage in this level
        float spacing = 150f;

        for (int i = 0; i < stages.Count; i++)
        {
            // Create the new Stage Button
            GameObject stageObject = new GameObject();
            Button stageButton = stageObject.AddComponent<Button>();

            int j = i; // Delegate argument for onClick listener StartStage()
            stageButton.onClick.AddListener(delegate { this.StartStage(j); });
            stageButton.interactable = i <= this.currentStage;

            // Set button label text
            TextMeshProUGUI stageButtonText = stageButton.gameObject.AddComponent<TextMeshProUGUI>();
            stageButtonText.text = stages[i].id;
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
