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
    private BattleManager battleManager;

    private StageData stageData;
    private TextMeshProUGUI countdownText;

    private float score;
    private int countdown = 3;
    private bool isStageOver = false;

    void Start()
    {
        // Cache the user and battle managers
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.battleManager = ModelLocator.GetModelInstance<BattleManager>() as BattleManager;

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
            float newScore = this.battleManager.RawScore;
            // Change score in HUD if needed
            if (this.score != newScore)
            {
                this.score = newScore;
                this.ScoreText.text = string.Format("Score: {0}", newScore);
            }

            // Check if stage is over
            this.isStageOver = this.battleManager.IsStageOver;
        }

        // Stage is newly over with results in the battle manager
        else if (!this.ResultOverlay.activeInHierarchy &&
            this.battleManager.StageResult != null)
        {
            // Get stage results
            List<ItemData> items = this.battleManager.StageResult.DroppedItems;

            // Give results to user manager
            this.UpdateExperience();
            this.userManager.GetUserData().AddItemsToInventory(items);

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
        // Tell battle manager it's safe to clear current stage data
        this.battleManager.ClearCache();
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
            // Disable countdown overlay
            this.CountdownOverlay.SetActive(false);

            // Tell battle manager the countdown is complete
            this.battleManager.IsCountdownComplete = true;
        }
    }

    // Increment user experience if score surpassed threshold
    private void UpdateExperience()
    {
        if (this.battleManager.MetaData.isComplete)
        {
            this.userManager.GetUserData().IncrementExperience();
        }
    }

    // Show results UI after TouchBoundary and EndBoundary have collided
    private void ShowResultOverlay()
    {
        // Cache ResultOverlay UI component and stage data
        ResultOverlay resultOverlay = this.ResultOverlay.GetComponent<ResultOverlay>();
        StageData stageData = this.battleManager.StageData;

        // Show score tier
        int[] scoreTier = stageData.scoreTier;
        resultOverlay.DisplayScoreTier(this.score, scoreTier);

        // Show dropped items
        resultOverlay.DisplayItemDrops(this.battleManager.StageResult.DroppedItems);

        // Turn on results UI
        this.ResultOverlay.SetActive(true);
    }
}
