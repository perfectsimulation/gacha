using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject RootPosition;

    private UserManager userManager;
    private int currentLevel;
    private int currentStage;

    private void Start()
    {
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.currentLevel = this.userManager.GetUserData().GetCurrentLevel();
        this.currentStage = this.userManager.GetUserData().GetCurrentStage();
        this.SetTitle();
        this.LayoutMenuWithStageButtons();
    }

    // Open a Stage scene from the Adventure scene
    public void StartStage(int stage)
    {
        // TODO: add argument for stage details
        // called onClick of a Stage Button
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log(stage);
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
