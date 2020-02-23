using System.Collections;
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
    private StageManager stageManager;

    private TextMeshProUGUI countdownText;

    private float score;
    private int countdown = 3;
    private bool isStageOver = false;

    void Start()
    {
        // Cache the user and stage managers
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;

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
            float newScore = this.stageManager.GetScore();
            // Change score in HUD if needed
            if (this.score != newScore)
            {
                this.score = newScore;
                this.ScoreText.text = string.Format("Score: {0}", newScore);
            }

            // Check if stage is over
            this.isStageOver = stageManager.IsStageOver();
        }
        else if (!this.ResultOverlay.activeInHierarchy)
        {
            // Stage is now over, get results from stage manager
            List<ItemData> droppedItems = this.stageManager.GetItemDrops();

            // Give results to user manager
            this.userManager.GetUserData().AddItemsToInventory(droppedItems);

            // Show result overlay
            this.ResultOverlay.SetActive(true);
            this.ShowResultOverlay();
        }

    }

    public void EndStage()
    {
        SaveAndExitStage();
    }

    private void SaveAndExitStage()
    {
        UserData userData = this.userManager.GetUserData();
        Persistence.SaveUserData(userData);

        // Tell stage manager to clear current stage data
        this.stageManager.ClearStage();

        // Load Adventure scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
            this.stageManager.SetCountdownComplete();
        }
    }

    // Show result overlay after stage ends
    private void ShowResultOverlay()
    {
        this.ResultOverlay.SetActive(true);
    }
}
