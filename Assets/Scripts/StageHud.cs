﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageHud : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject CountdownOverlay;
    public GameObject ResultOverlay;

    private UserManager userManager;
    private StoryManager storyManager;

    private StageData stageData;
    private TextMeshProUGUI countdownText;

    private float score;
    private int countdown = 3;
    private bool isStageOver = false;

    void Start()
    {
        // Cache the user and stage managers
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.storyManager = ModelLocator.GetModelInstance<StoryManager>() as StoryManager;

        // Show stage start countdown overlay
        this.ShowCountdownOverlay();

        // Hide stage results overlay
        this.ResultOverlay.SetActive(false);
    }

    void Update()
    {
        // Update score while stage is in session
        if (!this.isStageOver)
        {
            float newScore = this.storyManager.GetScore();
            // Change score in HUD if needed
            if (this.score != newScore)
            {
                this.score = newScore;
                this.ScoreText.text = string.Format("Score: {0}", newScore);
            }

            // Check if stage is over
            this.isStageOver = storyManager.IsStageOver();
        }
        else if (!this.ResultOverlay.activeInHierarchy)
        {
            // Stage is now over, get results from stage manager
            List<ItemData> droppedItems = this.storyManager.GetDroppedItems();

            // Give results to user manager
            this.UpdateExperience();
            this.userManager.GetUserData().AddItemsToInventory(droppedItems);

            // Show result overlay
            this.ResultOverlay.SetActive(true);
            this.ShowResultOverlay();
        }

    }

    public void EndStage()
    {
        this.SaveStage();
        this.ClearCurrentStageData();
        // Load Adventure scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void SaveStage()
    {
        UserData userData = this.userManager.GetUserData();
        Persistence.SaveUserData(userData);
    }

    private void ClearCurrentStageData()
    {
        // Tell stage manager to clear current stage data
        this.storyManager.ClearStage();
    }

    // Show countdown overlay before stage begins
    private void ShowCountdownOverlay()
    {
        this.CountdownOverlay.SetActive(true);
        this.countdownText = this.CountdownOverlay.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(BeginCountdown());
    }

    // Count down to begin stage
    private IEnumerator BeginCountdown()
    {
        // Set the countdown text
        this.countdownText.text = this.countdown.ToString();
        // Wait for one second
        yield return new WaitForSeconds(1f);
        // Decrement countdown
        this.countdown--;

        if (this.countdown > 0)
        {
            // Still more to count down
            StartCoroutine(BeginCountdown());
        }
        else
        {
            // Disable countdown overlay and set to true isCountdownComplete in stage manager
            this.CountdownOverlay.SetActive(false);
            this.storyManager.SetCountdownComplete();
        }
    }

    // Increment user and card experiences if score surpassed threshold
    private void UpdateExperience()
    {
        if (this.storyManager.GetMetaData().isComplete)
        {
            this.userManager.GetUserData().IncrementExperience();
        }
    }

    // Show result overlay after stage ends
    private void ShowResultOverlay()
    {
        ResultOverlay resultOverlay = this.ResultOverlay.GetComponent<ResultOverlay>();
        StageData stageData = this.storyManager.GetStageData();
        // Show score tier
        int[] scoreTier = stageData.scoreTier;
        resultOverlay.DisplayScoreTier(this.score, scoreTier);
        resultOverlay.DisplayItemDrops(this.storyManager.GetDroppedItems());
        this.ResultOverlay.SetActive(true);
    }
}
